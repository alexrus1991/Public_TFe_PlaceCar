import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormuleTypeService } from 'src/app/main/api/generated';

@Component({
    templateUrl: './ajouter-formule-type.component.html',
    providers: [MessageService]
})
export class AjouterFormuleTypeComponent implements OnInit, OnDestroy {

    formuleTypeForm: FormGroup;
    subscription: Subscription;
    idFormuleType: number;

    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, private formBuilder: FormBuilder, 
        private formuleTypeService: FormuleTypeService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute
    ) {
    }

    ngOnInit() {
        this.formuleTypeForm = this.formBuilder.group({
            forM_Title: ['', [Validators.required]],
            forM_Type_Description: [''],
          });
          
          this.activatedRoute.paramMap.subscribe(params => {
            this.idFormuleType = parseInt(params.get("idFormuleType")) ?? 0;
            this.idFormuleType = isNaN(this.idFormuleType) ? 0 : this.idFormuleType;
            if (this.idFormuleType > 0) {

                /*
                this.formuleTypeService.apiTypesDeFormulesGetById(this.idFormuleType).subscribe({
                    next: (value: TypeMedicamentOutput) => {
                        this.typeMedicamentForm = this.formBuilder.group({
                            typeMedoc: [value.typeMedoc, [Validators.required]],
                            description: [value.description],
                          });
                    },
                    error: (err) => {

                    }
                })*/
                
            }
        });
    }

    ngOnDestroy() {
        /*if (this.subscription) {
            this.subscription.unsubscribe();
        }*/
    }

    ajouter() {     
        this.formuleTypeForm.markAllAsTouched();
        if(this.formuleTypeForm.valid) {

            this.blockUI.start('Opération en cours ...');
            this.formuleTypeService.apiTypesDeFormulesPost(this.formuleTypeForm.value).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                    },
                    error : (err) => {
                        this.blockUI.stop();
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            )
        }
    }

    modifier() {
      
        /*this.formuleTypeForm.markAllAsTouched();
        if(this.formuleTypeForm.valid) {

            this.blockUI.start('Opération en cours ...');
            this.formuleTypeService.api(this.idTypeMedicament, this.typeMedicamentForm.value).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Modification effectuée avec succès' });
                    },
                    error : (err) => {
                        this.blockUI.stop();
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            )
        }*/
    }
}
