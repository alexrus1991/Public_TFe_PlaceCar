import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormulePrixService, FormuleTypeService, ParkingService, PaysService, ReadEmpWorkInDTO, ReadForulesParkingDTO, ReadPaysDTO, ReadTypeFormDTO } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './lister-formule-prix.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerFormulePrixComponent implements OnInit, OnDestroy {

    formulePrixForm: FormGroup;
    formuleTypeItems: ReadTypeFormDTO[] = [];
    formulePrixList: ReadForulesParkingDTO[] = [];
    loading: boolean = false;
    @ViewChild('filter') filter!: ElementRef;
    subscription: Subscription;
    idParking = 0;
    idFormulePrix = 0;
    afficherDialogFormulePrix = false;
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, private formBuilder: FormBuilder, 
        private formuleTypeService: FormuleTypeService,
        private formulePrixService: FormulePrixService,
        private confirmationService: ConfirmationService, 
        private messageService: MessageService,
        private parkingService: ParkingService,
        private paysService: PaysService,
        private authService: AuthService,
        private router: Router
    ) {
        this.blockUI.start('Chargement en cours ...');

        this.formulePrixForm = this.formBuilder.group({
            prix: ['', [Validators.required]],
            formuleType: ['', [Validators.required]],
          });

        this.formuleTypeService.apiTypesDeFormulesGet().subscribe({
            next : (result: ReadTypeFormDTO[]) => {
                console.log(result);
                this.formuleTypeItems = result;
            },
            error : (err) => {
            }
        });  

        this.parkingService.apiParkingsEmployeeEmployeeIdGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next : (result: ReadEmpWorkInDTO) => {
                this.blockUI.stop();
                this.idParking = result['parK_Id'];
                this.rechercher(this.idParking);
            },   
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });
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

    editer(idFormulePrix: number, prix: number, type: any) {
        //this.router.navigate(['gestion-formule-type/ajouter-formule-type', idFormuleType]);
        this.idFormulePrix = idFormulePrix;
        this.formulePrixForm.patchValue({
            'prix': prix,
            'formuleType': type,
        });
        this.afficherDialogFormulePrix = true;
    }

    supprimer(idFormuleType: number) {
        
    }

    rechercher(idParking: number) {
        this.loading = true;
        this.formulePrixService.apiFormuleDePrixParkingParkingIdGet(idParking).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.formulePrixList = result;
                    for (let index = 0; index < result.length; index++) {
                        const element = result[index];
                        
                        this.formuleTypeItems = this.formuleTypeItems.filter(function( obj ) {
                            return obj.title !== element.forM_Title;
                        });
                    }
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

    annulerFormulePrix() {
        this.afficherDialogFormulePrix = false;
    }

    ajouterFormuleDePrix() {
        this.idFormulePrix = 0;
        this.formulePrixForm.patchValue({
            'prix': '',
            'formuleType': ''
        });
        this.afficherDialogFormulePrix = true;
    }

    validerFormulePrix() {
        this.formulePrixForm.markAllAsTouched();
        if(this.formulePrixForm.valid) {

            console.log(this.formulePrixForm.value);

            this.blockUI.start('Opération en cours ...');
            if(this.idFormulePrix > 0) {

                this.formulePrixService.apiFormuleDePrixParkingParkingIdFormuleFormuleIdPut(this.idFormulePrix,
                    this.idParking, this.formulePrixForm.value.prix).subscribe(
                        {
                            next : (result) => {
                                this.blockUI.stop();
                                this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Modification effectuée avec succès' });
                                this.afficherDialogFormulePrix = false;
                                this.rechercher(this.idParking);
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
                this.formulePrixService.apiFormuleDePrixPost({
                    prix : this.formulePrixForm.value.prix.toString().replace(',', '.'),
                    typeId: this.formulePrixForm.value.formuleType.id,
                    parkingId: this.idParking 
                }).subscribe(
                    {
                        next : (result) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                            this.afficherDialogFormulePrix = false;
                            ;
                            this.rechercher(this.idParking);
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
}
