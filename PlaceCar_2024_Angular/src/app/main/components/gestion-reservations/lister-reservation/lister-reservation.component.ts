import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormulePrixService, PlaceService, ReadForulesParkingDTO, ReadPlaceLibDTO, ReservationService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './lister-reservation.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerReservationComponent implements OnInit, OnDestroy {

    reservationForm: FormGroup;
    dateForm: FormGroup;
    reservations: any[] = [];
    loading: boolean = true;
    @ViewChild('filter') filter!: ElementRef;
    subscription: Subscription;
    idParking = 0;
    idReservation = 0;
    formulePrixList: ReadForulesParkingDTO[] = [];
    placeLibreList: ReadPlaceLibDTO[] = [];
    afficherConfirmationReservationDialog = false;
    today = new Date();
    dateFin = new Date();
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private confirmationService: ConfirmationService, 
        private reservationService: ReservationService,
        private messageService: MessageService,
        private placeService: PlaceService,
        private formulePrixService: FormulePrixService,
        private authService: AuthService,
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

        this.dateForm = this.formBuilder.group({
            dateDebut: ['', [Validators.required]],
            dateFin: [''],
          });
        this.reservationForm = this.formBuilder.group({
            formulePrixList: [''],
            formulePrix: [''],
            formulePrixData: [{value: '', disabled : true}],
            placeLibreList: ['', [Validators.required]],
            placeLibre: [],
            placeLibreData: [{value: '', disabled : true}, [Validators.required]],
          });
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

        this.reservations = []; 
        this.rechercherReservationsCloturees();
    }

    rechercherReservationsCloturees() {
        this.reservationService.apiReservationsClientClientIdClotureGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next: (result) => {

                console.log('Transactions cloturées');
                console.log(result);
                
                for (let index = 0; index < result.length; index++) {
                    const element = result[index];
                    element['status'] = 'CLOTURE';
                    element['statusTag'] = 'qualified';

                    element['reS_DateReservation'] = element['reS_DateReservation']?.substring(0, 10);
                    element['reS_DateDebut'] = element['reS_DateDebut']?.substring(0, 10);
                    element['reS_DateFin'] = element['reS_DateFin']?.substring(0, 10);
                    this.reservations.push(element);
                }
                //this.loading = false;

                this.rechercherReservationsNonCloturees();
            },
            error: (err) => {
                this.loading = false;
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });
    }

    rechercherReservationsNonCloturees() {
        this.reservationService.apiReservationsClientClientIdNonClotureGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next: (result) => {

                console.log('Réservations non cloturées');
                console.log(result);

                for (let index = 0; index < result.length; index++) {
                    const element = result[index];
                    element['status'] = 'NON CLOTURE';
                    element['statusTag'] = 'unqualified';

                    element['reS_DateReservation'] = element['reS_DateReservation']?.substring(0, 10);
                    element['reS_DateDebut'] = element['reS_DateDebut']?.substring(0, 10);
                    element['reS_DateFin'] = element['reS_DateFin']?.substring(0, 10);
                    this.reservations.push(element);
                }
                this.loading = false;

                this.reservations.sort(function(a, b) {
                    return a.reS_Id- b.reS_Id
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

    cloturerReservation(idReservation: number) {

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
                this.reservationService.apiReservationsClientClientIdReservationReservationIdCloturePut(this.authService.getUser()['nameIdentifier'], idReservation).subscribe(
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
        });
    }

    supprimerReservation(idReservation: number) {

        this.confirmationService.confirm({
            key: 'confirmDialog',
            //target: event.target || new EventTarget,
            header: 'Confirmation',
            message: 'Voulez-vous vraiment supprimer cette réservation ?',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Oui',
            rejectLabel: 'Non',
            accept: () => {

                this.blockUI.start('Opération en cours ...');
                this.reservationService.apiReservationsClientClientIdReservationReservationIdSuprimerDelete(
                    idReservation,
                    this.authService.getUser()['nameIdentifier']).subscribe(
                    {
                        next: (value) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Réservation supprimée avec succès' });
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
        });
    }

    editerReservation(idReservation: number, idParking: number, dateDebut: any, dateFin: any) {
        this.idReservation = idReservation;
        this.idParking = idParking;
        this.dateFin = new Date(Date.parse(dateFin));
        this.formulePrixList = [];
        this.placeLibreList = [];
        console.log(dateDebut);
        console.log(dateFin);

        this.dateForm.patchValue({
            'dateDebut': new Date(Date.parse(dateDebut)),
            'dateFin': new Date(Date.parse(dateFin))
        });

        this.afficherConfirmationReservationDialog = true;
    }

    afficherDisponibilites() {

        if(this.idParking > 0) {
             
            this.dateForm.markAllAsTouched();
            if(this.dateForm.valid)  {
    
                this.reservationForm.reset();
                /*this.listerFormulePrix();
    
                this.placeService.apiPlacesParkingLidreParkingParkingidDateDateGet(this.idParking,
                    (this.dateForm.value.dateDebut as Date).toISOString()
                ).subscribe(
                    {
                        next : (result: ReadPlaceLibDTO[]) => {                  
                            this.placeLibreList = [];
    
                            for (let index = 0; index < result.length; index++) {
                                const element = result[index];
                                
                                element['data'] = 'Etage - ' + element.plA_Etage + ', P - ' + element.plA_NumeroPlace;
                                this.placeLibreList.push(element);
                            }
                           // this.placeLibreList = result;
                            //this.oPanel.toggle(null, document.querySelector('.address-col'));
                            this.afficherConfirmationReservationDialog = true;
                        },
                        error : (err) => {
                            console.log(err);
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                        }
                    }
                ); */    
            }
        }
        else {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: "Veillez sélectionner un parking." });
        }
    }

    listerFormulePrix() {

        this.formulePrixService.apiFormuleDePrixParkingParkingIdGet(this.idParking).subscribe(
            {
                next : (result: ReadForulesParkingDTO[]) => {
                    this.formulePrixList = [];
                    for (let index = 0; index < result.length; index++) {
                        const element = result[index];
                        
                        element['data'] = element.forM_Title + ' - ' + element.forM_Prix + ' €';
                        this.formulePrixList.push(element);
                    }
                    //this.formulePrixList = result;
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );
    }

    onDateDebutChange(event: any) {
        //this.onParkingClick(this.parking);
        this.formulePrixList = [];
        this.placeLibreList = [];
    }

    onPlaceLibreChange(placeLibre: any) {
        this.reservationForm.patchValue({
            'placeLibre': placeLibre,
            'placeLibreData': placeLibre?.data
        });
    }

    onFormulePrixChange(formulePrix: any) {
        this.reservationForm.patchValue({
            'formulePrix': formulePrix,
            'formulePrixData': formulePrix?.data
        });
    }

    validerModification() {

        this.reservationForm.markAllAsTouched();

        if(this.reservationForm.valid) {

            if(this.dateForm.value.dateDebut > this.dateForm.value.dateFin) {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: "Date de début de la réservation ne peut pas être supérieure à la date de fin." });
                return;
            }

            if(this.dateForm.value.dateFin < this.dateFin) {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: "Date de fin choisie de la réservation ne peut pas être inférieur à la date de fin de la réservation actuelle." });
                return;
            }
            
            let reservation = {
                "reS_DateDebut": (this.dateForm.value.dateDebut as Date)?.toISOString(),
                "reS_DateFin": this.dateForm.value.dateFin === '' || this.dateForm.value.dateFin === null ? null : (this.dateForm.value.dateFin as Date).toISOString(),
                "placeId":  this.reservationForm.value.placeLibre.plA_Id ,
                "clientId": this.authService.getUser()['nameIdentifier'],
                "formPrixId": (this.reservationForm.value.formulePrix === '' || this.reservationForm.value.formulePrix === null) ? 0 : this.reservationForm.value.formulePrix.forM_Id,
            }

            /*if(!this.authService.isLoggedIn()) {
                this.afficherConfirmationReservationDialog = false;
                return;
            }*/

            this.blockUI.start('Mise à jour en cours ...');
            this.reservationService.apiReservationsClientClientIdPut(this.authService.getUser()['nameIdentifier'], {
                clientId: this.authService.getUser()['nameIdentifier'],
                formPrixId: reservation.formPrixId,
                placeId: reservation.placeId,
                reS_DateFin: reservation.reS_DateFin,
                reS_Id: this.idReservation,
            }).subscribe(
                {
                    next : (result) => {
                       
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Réservation modifiée avec succès'});
                        this.afficherConfirmationReservationDialog = false;

                        this.rechercher();
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
            this.messageService.add({ severity: 'error', summary: 'Information', detail: "Veillez sélectionner une place et une formule de prix." });
        }         
    }
}
