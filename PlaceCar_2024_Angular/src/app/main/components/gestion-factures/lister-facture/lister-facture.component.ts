import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FactureService, ReservationService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './lister-facture.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerFactureComponent implements OnInit, OnDestroy {

    factures: any[] = [];
    loading: boolean = true;
    @ViewChild('filter') filter!: ElementRef;
    subscription: Subscription;
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private confirmationService: ConfirmationService, 
        private reservationService: ReservationService,
        private factureService: FactureService,
        private messageService: MessageService,
        private authUser: AuthService,
        public router: Router
    ) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/
       this.rechercher();
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        /*if (this.subscription) {
            this.subscription.unsubscribe();
        }*/
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }

    rechercher() {

        this.factures = []; 
        this.rechercherFacturesPayes();
        
    }

    rechercherFacturesPayes() {
        this.factureService.apiFacturesClientClientIdPayeGet(this.authUser.getUser()['nameIdentifier']).subscribe({
            next: (result) => {

                console.log('Factures payés');
                console.log(result);

                for (let index = 0; index < result.length; index++) {
                    const element = result[index];
                    element['status'] = 'PAYE';
                    element['statusTag'] = 'qualified';

                    element['facT_Date'] = element['facT_Date']?.substring(0, 10);
                    element['reS_DateReservation'] = element['reS_DateReservation']?.substring(0, 10);
                    element['reS_DateDebut'] = element['reS_DateDebut']?.substring(0, 10);
                    element['reS_DateFin'] = element['reS_DateFin']?.substring(0, 10);
                    this.factures.push(element);
                }

                this.rechercherFacturesNonPayes();
                //this.loading = false;
            },
            error: (err) => {
                this.loading = false;
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });

    }

    rechercherFacturesNonPayes() {
        this.factureService.apiFacturesClientClientIdNonPayeGet(this.authUser.getUser()['nameIdentifier']).subscribe({
            next: (result) => {
                console.log('Factures non payés');
                console.log(result);

                for (let index = 0; index < result.length; index++) {
                    const element = result[index];
                    element['status'] = 'NON PAYE';
                    element['statusTag'] = 'unqualified';

                    element['facT_Date'] = element['facT_Date']?.substring(0, 10);
                    element['reS_DateReservation'] = element['reS_DateReservation']?.substring(0, 10);
                    element['reS_DateDebut'] = element['reS_DateDebut']?.substring(0, 10);
                    element['reS_DateFin'] = element['reS_DateFin']?.substring(0, 10);
                    console.log(element);
                    this.factures.push(element);
                }
                this.loading = false;

                this.factures.sort(function(a, b) {
                    return a.facT_Id- b.facT_Id
                });
            },
            error: (err) => {
                this.loading = false;
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });
    }

    payerFacture(idFacture: number) {

        let facture = this.factures.find(o => o.facT_Id === idFacture);
        this.router.navigateByUrl('/gestion-factures/payer-facture', { state: { facture } });


        /*
        this.confirmationService.confirm({
            key: 'confirmDialog',
            //target: event.target || new EventTarget,
            header: 'Confirmation',
            message: 'Voulez-vous vraiment clôturer cette réservation ?',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Oui',
            rejectLabel: 'Non',
            accept: () => {

                this.blockUI.start('Opération en cours ...');
                this.reservationService.apiReservationsClientClientIdReservationReservationIdCloturePut(this.authUser.getUser()['nameIdentifier'], IdReservation).subscribe(
                    {
                        next: (value) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Réservation cloturée avec succès' });
                            this.rechercher();
                        },
                        error: (err) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message });
                        },
                    }
                );
            },
            reject: () => {
            }
        });*/
    }
}
