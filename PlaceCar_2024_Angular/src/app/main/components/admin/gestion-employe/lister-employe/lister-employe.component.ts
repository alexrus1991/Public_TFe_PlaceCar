import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { EmplyeeService, ParkingService, PaysService, ReadPaysDTO } from 'src/app/main/api/generated';

@Component({
    templateUrl: './lister-employe.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerEmployeComponent implements OnInit {

    utilisateurForm: FormGroup;
    employes: any[] = [];
    rechercheForm: FormGroup;
    paysItems: ReadPaysDTO[];
    villeItems: any[];
    parkingItems: any[];
    paysItems2: ReadPaysDTO[];
    villeItems2: any[];
    parkingItems2: any[];
    loading: boolean = false;
    idParking = 0;
    idEmploye = 0;
    detail = '';
    nomEmploye = '';
    prenomEmploye = '';
    afficherDialogChangerParking = false;
    @ViewChild('filter') filter!: ElementRef;

    subscription: Subscription;
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder,
        private paysService: PaysService,
        private parkingService: ParkingService,
        private messageService: MessageService,
        private emplyeeService: EmplyeeService,
        private confirmationService: ConfirmationService, 
        private router: Router) {
        
        let state = this.router.getCurrentNavigation()?.extras?.state;
        console.log(state);
        if(state) {
            this.idParking = state['idParking'];
            this.detail = state['detail'];   
        }
    }

    ngOnInit() {

        this.rechercheForm = this.formBuilder.group({
            pays: ['', [Validators.required]],
            ville: ['', [Validators.required]],
            parking: ['', [Validators.required]],
          });

          this.utilisateurForm = this.formBuilder.group({
            pays: ['', [Validators.required]],
            ville: ['', [Validators.required]],
            parking: ['', [Validators.required]],
          });

        if(this.idParking > 0) {
            this.rechercheForm = this.formBuilder.group({
                pays: [''],
                ville: [''],
                parking: [''],
              });
            this.rechercher(this.idParking); 
        }
        else {

            //idParking est égal à 0
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
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }

    changerEmployeParking(idEmploye: number, nomEmploye: string, prenomEmploye: string) {
        console.log(idEmploye);
        this.idEmploye = idEmploye;
        this.nomEmploye = nomEmploye;
        this.prenomEmploye = prenomEmploye;

        this.paysService.apiPaysGet().subscribe({
            next : (result: ReadPaysDTO[]) => {
                console.log(result);
                this.paysItems2 = result;
                //this.loading = false;
            },
            error : (err) => {
                //this.loading = false;
                //this.errorMessage = err.error ? err.error.message ?? err.message : err.message;
            }
        }); 

       // this.router.navigateByUrl('/admin/gestion-employe/changer-parking-employe', { state: { idEmploye, nomEmploye, prenomEmploye } });
       this.afficherDialogChangerParking = true;
    }

    suprimerEmploye(idEmploye: number) {

        this.confirmationService.confirm({
            key: 'confirmDialog',
            //target: event.target || new EventTarget,
            header: 'Confirmation',
            message: 'Voulez-vous vraiment supprimer cet employé ?',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Oui',
            rejectLabel: 'Non',
            accept: () => {

                this.blockUI.start('Opération en cours ...');
                this.emplyeeService.apiEmployeesEmployeeEmployeeIdDelete(idEmploye).subscribe(
                    {
                        next : (result) => {
                            this.blockUI.stop();
                            console.log(result);
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Employé supprimé avec succès' });
                            this.rechercher(this.rechercheForm.value.parking.parK_Id);
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

    onPaysChange(event) {
        this.villeItems = [];
        this.parkingService.apiParkingsVillesPaysPaysIdGet(event.value.payS_Id).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.villeItems = result;
                    this.rechercher(this.rechercheForm.value.parking.parK_Id);
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );       
    }

    onPaysChange2(event) {
        this.villeItems2 = [];
        this.parkingService.apiParkingsVillesPaysPaysIdGet(event.value.payS_Id).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.villeItems2 = result;
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );       
    }

    onVilleChange(event) {

        this.parkingItems = [];
        this.employes = [];
        this.parkingService.apiParkingsPaysPaysIdVilleNomVilleGet(
            this.rechercheForm.value.pays.payS_Id,
            this.rechercheForm.value.ville).subscribe(
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

    onVilleChange2(event) {

        this.parkingItems2 = [];
        this.parkingService.apiParkingsPaysPaysIdVilleNomVilleGet(
            this.utilisateurForm.value.pays.payS_Id,
            this.utilisateurForm.value.ville).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.parkingItems2 = result;
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );
    }

    onParkingChange(event) {
        
        this.employes = [];
        console.log(event.value);
        this.rechercher(this.rechercheForm.value.parking.parK_Id);
    }

    rechercher(idParking: number) {
        
        if(this.rechercheForm.valid) {
            // La recherche est valide c'est à dire le pays, la ville et le parking ont été renseignés suivant la condition 'required ou pas'
            this.loading = true;
            this.emplyeeService.apiEmployeesParkingParkingIdGet(idParking).subscribe(
                {
                    next : (result) => {
                        console.log(result);
                        this.employes = result;
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

    annulerChangerParking() {
        this.afficherDialogChangerParking = false;
    }

    validerChangerParking() {
      
        this.utilisateurForm.markAllAsTouched();
        if(this.utilisateurForm.valid && this.idEmploye > 0) {

            this.blockUI.start('Opération en cours ...');
            this.emplyeeService.apiEmployeesParkingParkingIdChangementPut(this.utilisateurForm.value.parking.parK_Id, this.idEmploye).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.afficherDialogChangerParking = false;
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Parking changé avec succès' });

                        this.rechercher(this.rechercheForm.value.parking.parK_Id);
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
}
