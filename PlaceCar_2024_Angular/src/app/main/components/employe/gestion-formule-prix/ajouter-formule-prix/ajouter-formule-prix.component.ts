import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormulePrixService, FormuleTypeService, ParkingService, PaysService, ReadEmpWorkInDTO, ReadPaysDTO, ReadTypeFormDTO } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './ajouter-formule-prix.component.html',
    providers: [MessageService]
})
export class AjouterFormulePrixComponent implements OnInit {

    formulePrixForm: FormGroup;
    subscription: Subscription;
    idFormulePrix: number;
    formuleTypeItems: ReadTypeFormDTO[] = [];
    idParking = 0;


    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, private formBuilder: FormBuilder, 
        private formulePrixService: FormulePrixService,
        private formuleTypeService: FormuleTypeService,
        private messageService: MessageService,
        private parkingService: ParkingService,
        private paysService: PaysService,
        private authService: AuthService,
        private activatedRoute: ActivatedRoute
    ) {

        this.formuleTypeService.apiTypesDeFormulesGet().subscribe({
            next : (result: ReadTypeFormDTO[]) => {
                console.log(result);
                this.formuleTypeItems = result;
            },
            error : (err) => {
            }
        });   

        //this.blockUI.start('Chargement en cours ...');

        this.parkingService.apiParkingsEmployeeEmployeeIdGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next : (result: ReadEmpWorkInDTO) => {
                this.blockUI.stop();
                this.idParking = result['parK_Id'];
            },   
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });
    }

    ngOnInit() {

        this.formulePrixForm = this.formBuilder.group({
            prix: ['', [Validators.required]],
            formuleType: ['', [Validators.required]],
          });
    }

    ajouter() {     
        this.formulePrixForm.markAllAsTouched();
        if(this.formulePrixForm.valid) {

            if(this.idParking > 0) {
                console.log(this.formulePrixForm.value);
                this.blockUI.start('Opération en cours ...');
                this.formulePrixService.apiFormuleDePrixPost({
                    prix : this.formulePrixForm.value.prix.toString().replace(',', '.'),
                    typeId: this.formulePrixForm.value.formuleType.id,
                    parkingId: this.idParking 
                }).subscribe(
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
                );
            }
            else {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: "Vous n'êtes pas employé d'un parking"});
            }   
            
        }
      
    }

    modifier() {
      
       
    }
}
