import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormuleTypeService, ReadTypeFormDTO } from 'src/app/main/api/generated';

@Component({
    templateUrl: './lister-formule-type.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerFormuleTypeComponent implements OnInit, OnDestroy {

   
    formuleTypeList: ReadTypeFormDTO[] = [];
    formuleTypeForm: FormGroup;
    loading: boolean = true;
    @ViewChild('filter') filter!: ElementRef;
    subscription: Subscription;
    @BlockUI() blockUI: NgBlockUI;
    afficherDialogAjouterFormuleType = false;
    idFormuleType = 0;
    isEditerTitre = false;
    isEditerDescription= false;

    constructor(private layoutService: LayoutService, private formBuilder: FormBuilder, 
        private formuleTypeService: FormuleTypeService,
        private confirmationService: ConfirmationService, private messageService: MessageService,
        private router: Router
    ) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/
    }

    ngOnInit() {

        this.formuleTypeForm = this.formBuilder.group({
            forM_Title: ['', [Validators.required]],
            forM_Type_Description: [''],
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
        this.loading = true;
        this.formuleTypeService.apiTypesDeFormulesGet().subscribe({
            next : (result: ReadTypeFormDTO[]) => {
                console.log(result);
                this.formuleTypeList = result;
                this.loading = false;
            },
            error : (err) => {
                this.loading = false;
            }
        });    
    }

    editerTitre(idFormuleType: number, titre: string, description: string) {
        this.idFormuleType = idFormuleType;
        this.isEditerTitre = true;
        this.isEditerDescription = false;
          this.formuleTypeForm.patchValue({
            'forM_Title': titre,
            'forM_Type_Description': description
          });
          this.afficherDialogAjouterFormuleType = true;
    }

    editerDescription(idFormuleType: number, titre: string, description: string) {
        this.idFormuleType = idFormuleType;
        this.isEditerTitre = false;
        this.isEditerDescription = true;
        this.formuleTypeForm.patchValue({
            'forM_Title': titre,
            'forM_Type_Description': description
          });
          this.afficherDialogAjouterFormuleType = true;
    }

    supprimer(idFormuleType: number) {
        
        this.confirmationService.confirm({
            key: 'confirmDialog',
            //target: event.target || new EventTarget,
            header: 'Confirmation',
            message: 'Voulez-vous vraiment supprimer ce type ?',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Oui',
            rejectLabel: 'Non',
            accept: () => {

                /*this.blockUI.start('Opération en cours ...');
                this.formuleTypeService.apiTypesDeFormulesGetDelete(idFormuleType).subscribe(
                    {
                        next: (value) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Suppression effectuée avec succès' });
                            this.rechercher();
                        },
                        error: (err) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message });
                        },
                    }
                )*/
            },
            reject: () => {
            }
        });
    }

    ajouterFormuleType() {
        this.idFormuleType = 0;
        this.isEditerTitre = true;
        this.isEditerDescription = true;
        this.afficherDialogAjouterFormuleType = true;
    }

    annuler() {
        this.afficherDialogAjouterFormuleType = false;
    }

    valider() {   
        this.formuleTypeForm.markAllAsTouched();     
        if(this.formuleTypeForm.valid) {

            this.blockUI.start('Opération en cours ...');

            if(this.idFormuleType == 0) {
                this.formuleTypeService.apiTypesDeFormulesPost(this.formuleTypeForm.value).subscribe(
                    {
                        next : (result) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                            this.afficherDialogAjouterFormuleType = false;
    
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

                if(this.isEditerTitre === true) {
                    this.formuleTypeService.apiTypesDeFormulesUpdateTitreTypeIdPut(this.idFormuleType, 
                        this.formuleTypeForm.value.forM_Title).subscribe(
                        {
                            next : (result) => {
                                this.blockUI.stop();
                                this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Modification effectuée  avec succès' });
                                this.afficherDialogAjouterFormuleType = false;
        
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
                    this.formuleTypeService.apiTypesDeFormulesUpdateDescriptionTypeIdPut(this.idFormuleType, 
                        this.formuleTypeForm.value.forM_Type_Description).subscribe(
                        {
                            next : (result) => {
                                this.blockUI.stop();
                                this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Modification effectuée avec succès' });
                                this.afficherDialogAjouterFormuleType = false;
        
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
                
            }
            
        }
    }
}
