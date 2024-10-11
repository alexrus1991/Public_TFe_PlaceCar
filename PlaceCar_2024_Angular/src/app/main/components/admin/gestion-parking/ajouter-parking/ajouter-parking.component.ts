import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { FormuleTypeService, ParkingService, PaysService, ReadPaysDTO } from 'src/app/main/api/generated';

@Component({
    templateUrl: './ajouter-parking.component.html',
    providers: [MessageService]
})
export class AjouterParkingComponent implements OnInit, OnDestroy {

    parkingForm: FormGroup;
    subscription: Subscription;
    idParking: number;
    paysItems: ReadPaysDTO[] = [];

    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, private formBuilder: FormBuilder, 
        private paysService: PaysService,
        private parkingService: ParkingService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute
    ) {
    }

    ngOnInit() {

        this.parkingForm = this.formBuilder.group({
            parK_Nom: ['', [Validators.required]],
            parK_NbEtages: ['', [Validators.required]],
            parK_NbPlaces: ['', [Validators.required]],
            adrS_Numero: ['', [Validators.required]],
            adrS_NomRue: ['', [Validators.required]],
            adrS_Ville: ['', [Validators.required]],
            pays: ['', [Validators.required]],
          });

          this.paysService.apiPaysGet().subscribe({
            next: (value: ReadPaysDTO[]) => {
                this.paysItems = value;
            },
            error: (err) => {

            }
          });
          
          this.activatedRoute.paramMap.subscribe(params => {
            this.idParking = parseInt(params.get("idPays")) ?? 0;
            this.idParking = isNaN(this.idParking) ? 0 : this.idParking;
            if (this.idParking > 0) {

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
        this.parkingForm.markAllAsTouched();
       
        if(this.parkingForm.valid) {

            let parkingData = {
                parK_Nom: this.parkingForm.value.parK_Nom,
                parK_NbEtages: this.parkingForm.value.parK_NbEtages,
                parK_NbPlaces: this.parkingForm.value.parK_NbPlaces,
                adrS_Numero: this.parkingForm.value.adrS_Numero,
                adrS_NomRue: this.parkingForm.value.adrS_NomRue,
                adrS_Ville: this.parkingForm.value.adrS_Ville,
                adrS_Latitude: 0,
                adrS_Longitude: 0,
                payS_Id: this.parkingForm.value.pays['payS_Id']
            };
            
            this.blockUI.start('Opération en cours ...');
            this.parkingService.apiParkingsPost(parkingData).subscribe(
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
