import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MenuItem, MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ParkingService, PaysService, ReadPaysDTO, UserService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './ajouter-admin.component.html',
    providers: [MessageService]
})
export class AjouterAdminComponent implements OnInit {

    utilisateurForm: FormGroup;
    subscription: Subscription;
    idUtilisateur: number = 0;
    paysItems: ReadPaysDTO[];
    villeItems: any[];
    parkingItems: any[];
    detail: '';
    items: MenuItem[];
    activeIndex: number = 0;
    
    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private paysService: PaysService,
        private parkingService: ParkingService,
        private userService: UserService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute,
        private authService: AuthService,
        private router: Router
    ) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/ 

            this.activatedRoute.paramMap.subscribe(params => {
                this.idUtilisateur = parseInt(params.get("idUtilisateur")) ?? 0;
                this.idUtilisateur = isNaN(this.idUtilisateur) ? 0 : this.idUtilisateur;
                console.log(this.idUtilisateur);
            });

            this.paysService.apiPaysGet().subscribe({
                next : (result: ReadPaysDTO[]) => {
                    console.log(result);
                    this.paysItems = result;
                    //this.loading = false;
                },
                error : (err) => {
                    //this.loading = false;
                    //this.errorMessage = err.error ? err.error.message ?? err.message : err.message;
                }
            });            
    }

    ngOnInit() {

        this.items = [
            {
                label: 'Sélection du pays',
            },
            {
                label: 'Sélection de la ville',
            },
            {
                label: 'Sélection du parking',
            },
            {
                label: 'Données utilisateur',
            },
        ];
        
        this.utilisateurForm = this.formBuilder.group({
            nom: ['', [Validators.required]],
            prenom: ['', [Validators.required]],
            dateNaissance: ['', [Validators.required]],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required]],
            pays: ['', [Validators.required]],
            ville: ['', [Validators.required]],
            parking: ['', [Validators.required]],
          });
    }

    creer() {
      
        this.utilisateurForm.markAllAsTouched();
        if(this.utilisateurForm.valid) {

            this.blockUI.start('Opération en cours ...');
            this.userService.apiUsersNouveauAdminPost({
                    parkingId: this.utilisateurForm.value.parking.parK_Id,
                    perS_DateNaissance: new Date((this.utilisateurForm.value.dateNaissance as Date).getTime() + (24 * 60 * 60 * 1000)).toISOString(),
                    perS_Nom: this.utilisateurForm.value.nom,
                    perS_Prenom: this.utilisateurForm.value.prenom,
                    perS_Email: this.utilisateurForm.value.email,
                    perS_Password: this.utilisateurForm.value.password
                }
            ).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Admin crée avec succès' });

                        this.utilisateurForm.reset();
                    },
                    error : (err) => {
                        this.blockUI.stop();
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            );
        }
    }

    modifier() {
      
    }

    onPaysChange(event) {
        console.log('event :' + event);
        console.log(event.value);

        this.parkingService.apiParkingsVillesPaysPaysIdGet(event.value.payS_Id).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.villeItems = result;
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );       
    }

    onVilleChange(event) {
        console.log('event :' + event);
        console.log(event.value);

        this.parkingItems = [];
        this.parkingService.apiParkingsPaysPaysIdVilleNomVilleGet(
            this.utilisateurForm.value.pays.payS_Id,
            this.utilisateurForm.value.ville).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.parkingItems = result;
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );
    }

    suivant() {

        if(this.activeIndex == 0) {
            this.utilisateurForm.markAllAsTouched();
            if(this.utilisateurForm.value.pays !== '') {
                this.activeIndex = 1;
            }
        }
        else if(this.activeIndex == 1) {
            this.utilisateurForm.markAllAsTouched();
            if(this.utilisateurForm.value.ville !== '') {
                this.activeIndex = 2;
            }
        }
        else if(this.activeIndex == 2) {
            this.utilisateurForm.markAllAsTouched();
            if(this.utilisateurForm.value.parking !== '') {
                this.activeIndex = 3;
            }
        }
        else if(this.activeIndex == 3) {
            this.utilisateurForm.markAllAsTouched();
            if(this.utilisateurForm.valid) {
                this.creer();
            }
        }
    }
}
