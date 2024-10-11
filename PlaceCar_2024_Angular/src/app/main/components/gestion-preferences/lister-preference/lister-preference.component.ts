import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FactureService, PreferenceService, ReservationService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './lister-preference.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerPreferenceComponent implements OnInit, OnDestroy {

    rechercheForm: FormGroup;
    preferences: any[] = [];
    parkingItems: any[] = [];
    loading: boolean = false;
    @ViewChild('filter') filter!: ElementRef;
    subscription: Subscription;
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private confirmationService: ConfirmationService, 
        private reservationService: ReservationService,
        private factureService: FactureService,
        private preferenceService: PreferenceService,
        private messageService: MessageService,
        private authUser: AuthService,
        public router: Router
    ) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/
       
    }

    ngOnInit() {

        this.rechercheForm = this.formBuilder.group({
            parking: ['', [Validators.required]],
          });

          this.rechercher();
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

        this.reservationService.apiReservationsClientClientIdClotureGet(this.authUser.getUser()['nameIdentifier']).subscribe({
            next: (result) => {
                console.log(result);
                this.parkingItems = [];

                for (let index = 0; index < result.length; index++) {
                    const element = result[index];

                    if(this.parkingItems.filter((p) => p.parK_Nom === element['parK_Nom'])?.length > 0) {
                        continue;
                    }
                    element['data'] =  element['parK_Nom'] + ', ' + element['adrS_Ville'];
                    this.parkingItems.push(element);
                }
               
                this.loading = false;
            },
            error: (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });
    }

    suprimerPreference(placeId: number, parkingId: number) {

        this.confirmationService.confirm({
            key: 'confirmDialog',
            //target: event.target || new EventTarget,
            header: 'Confirmation',
            message: 'Voulez-vous vraiment supprimer cette préférence ?',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Oui',
            rejectLabel: 'Non',
            accept: () => {

                this.blockUI.start('Opération en cours ...');
                this.preferenceService.apiPreferencesPlacePlaceIdDelete(placeId.toString(), {
                    clientId: this.authUser.getUser()['nameIdentifier'],
                    parkingId: parkingId,
                    placeId: placeId
                }).subscribe(
                    {
                        next : (result) => {
                            this.blockUI.stop();
                            console.log(result);
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Préférence supprimée avec succès' });
                            this.onParkingChange(null);
                        },
                        error : (err) => {
                            console.log(err);
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                        }
                    }
                );
            },
            reject: () => {
            }
        });
    }

    onParkingChange(event: any) {

        console.log(this.rechercheForm.value);

        this.loading = true;
        this.preferenceService.apiPreferencesClientClientIdParkingParkIdGet(this.rechercheForm.value.parking.parK_Id, this.authUser.getUser()['nameIdentifier']).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.preferences = result;
                    this.loading = false;
                },
                error : (err) => {
                    console.log(err);
                    this.loading = false;
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );
    }
}
