import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { CompteService,  ParkingService, PaysService, ReadPaysDTO, ReservationService, TrensactionService, UserService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';
/********************Strip Import and variables ***************/
import { StripeService } from 'src/app/main/api/generated';
import { ISession } from './stripeModel';
import { Guid } from "guid-typescript";

declare const Stripe;
/*************************************************************/

@Component({
    templateUrl: './payer-facture.component.html',
    providers: [MessageService]
})
export class PayerFactureComponent implements OnInit {

    paiementForm: FormGroup;
    compteItems: any[] = [];
    //factureItems: any[] = [];
    facture: any;
    reservation: any;
    afficherDialogCreerCompte = false;
    afficherDialogDetailFacture = false;
    compteBancaire = '';

    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService,
        private formBuilder: FormBuilder,
        private paysService: PaysService,
        private parkingService: ParkingService,
        private userService: UserService,
        private compteService: CompteService,
        private transactionService: TrensactionService,
        private reservationService: ReservationService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute,
        private authService: AuthService,
        private router: Router,
        private stripeservice:StripeService
    ) {

        let state = this.router.getCurrentNavigation()?.extras?.state;
        if(state) {
            this.facture = this.router.getCurrentNavigation().extras?.state['facture'];
            this.facture['data'] = 'Id facture: ' + this.facture['facT_Id'] + ', Date réservation: ' + this.facture['reS_DateReservation'] + ', Montant: ' + this.facture['facT_Somme'] + ' euros';
            //this.factureItems.push(this.facture);
        }

       console.log(this.facture);
    }

    ngOnInit() {

        this.paiementForm = this.formBuilder.group({
            compte: ['', [Validators.required]],
            facture: [{value: this.facture?.data, disabled: true}, [Validators.required]],
            enregistrerPreference: [''],
            communication: ['']
          });
          this.rechercherCompte();
    }

    rechercherCompte() {
        this.compteService.apiComptesClientClientIdInformationGet(this.authService.getUser()['nameIdentifier']).subscribe(
            {
                next : (result) => {
                    console.log(result);

                    this.blockUI.stop();
                    this.compteItems = [];
                    if(result != null) {
                        result['data'] = result['cB_Nom'] + ' - ' + result['cB_NumCompte'];

                        this.paiementForm.patchValue({
                            'compte' : result
                        });
                        this.compteItems.push(result);
                    }
                },
                error : (err) => {
                    this.blockUI.stop();
                    console.log(err);
                    //this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );
    }

    payer() {
        if(this.paiementForm.valid) {

            const data = {
                cb_NumCompte_Client : this.paiementForm.value.compte.cB_NumCompte,
                compteUnId: this.paiementForm.value.compte.cB_Id,
                factureId: this.facture['facT_Id'],
                somme: this.facture['facT_Somme'],
                tranS_Communication:  this.paiementForm.value.communication,
                Preference: this.paiementForm.value.enregistrerPreference?.length >  0 && this.paiementForm.value.enregistrerPreference[0] === '1'
              };
               const id =Guid.create();
               sessionStorage.setItem(id.toString(), JSON.stringify(data));

            //creation de transaction
            this.transactionService.apiTransactionsPost(data).subscribe(
                (response) => {
                    console.log("Transaction enregistrée avec succès", response);
                    //Paiement stripe

            const stripData={
                priceId: Math.round((this.facture['facT_Somme'])*100), //Stripe n'accepte que des nombres sans virgule, on doit multiplier par 100 le prix
                successUrl: `http://localhost:4200/gestion-factures/success?sessionId=${id}`,
                failureUrl: `http://localhost:4200/gestion-factures/failure?sessionId=${id}`,
                customerId:(JSON.parse(sessionStorage.getItem('placecar.user'))).emailaddress,
                factureId:`${this.facture['facT_Id']}`,
                communication:this.paiementForm.value.communication,
                compteUnId:this.paiementForm.value.compte.cB_Id
            }

            this.stripeservice.apiStripeCreateCheckoutSessionPost(stripData).subscribe
            ( {
                        next : (result) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Redirection vers Stripe' });
                            this.redirectToCheckout(result);

                        },
                        error : (err) => {
                            this.blockUI.stop();
                            console.log(err);
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                        }
                    }
                );
                },
                (error) => {
                    console.error("Erreur lors de l'enregistrement de la transaction", error);
                }
            );

            
        }
    }

    redirectToCheckout(session: ISession) {
        const stripe = Stripe(session.publicKey);

        stripe.redirectToCheckout({
          sessionId: session.sessionId,
        });
      }

    annulerCreationCompte() {
        this.afficherDialogCreerCompte = false;
        this.compteBancaire = '';
    }

    creerCompteBancaire() {

        this.afficherDialogCreerCompte = true;
    }

    doCreerCompteBancaire() {

        if (this.compteBancaire && this.compteBancaire.trim().length > 0) {

            this.afficherDialogCreerCompte = false;
            this.blockUI.start('Création du compte en cours ...');
            this.compteService.apiComptesClientClientIdPost(this.authService.getUser()['nameIdentifier'], {
                cB_Nom : this.compteBancaire,
                clientId: this.authService.getUser()['nameIdentifier']
            }).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Compte crée avec succès'});
                        this.rechercherCompte();
                    },
                    error : (err) => {
                        this.blockUI.stop();
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            );
        }
        else {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: 'Veuillez renseigner le nom du compte' });
        }
    }

    detailFacture() {
        if(this.facture) {
            this.afficherDialogDetailFacture = true;
        }
    }
}
