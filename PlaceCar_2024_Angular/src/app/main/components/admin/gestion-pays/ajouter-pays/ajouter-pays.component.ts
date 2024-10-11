import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormuleTypeService, PaysService } from 'src/app/main/api/generated';

@Component({
    templateUrl: './ajouter-pays.component.html',
    providers: [MessageService]
})
export class AjouterPaysComponent implements OnInit, OnDestroy {

    paysForm: FormGroup;
    subscription: Subscription;
    idPays: number;

    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, private formBuilder: FormBuilder, 
        private paysService: PaysService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute
    ) {
    }

    ngOnInit() {
        this.paysForm = this.formBuilder.group({
            nomPays: ['', [Validators.required]],
          });
          
          this.activatedRoute.paramMap.subscribe(params => {
            this.idPays = parseInt(params.get("idPays")) ?? 0;
            this.idPays = isNaN(this.idPays) ? 0 : this.idPays;
            if (this.idPays > 0) {

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
        this.paysForm.markAllAsTouched();
        if(this.paysForm.valid) {

            this.blockUI.start('Opération en cours ...');
            this.paysService.apiPaysPost(this.paysForm.value.nomPays).subscribe(
                {
                    next : (result) => {
                        console.log(result);
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                    },
                    error : (err) => {
                        this.blockUI.stop();
                        console.log(err);

                        if(err['status'] === 201) {
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                        }
                        else {
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});

                        }
                    },
                    complete() {
                        this.blockUI.stop();

                    },
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
