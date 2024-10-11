import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ParkingService, PaysService, ReadPaysDTO } from 'src/app/main/api/generated';

@Component({
    templateUrl: './lister-parking.component.html',
    providers: [MessageService]
})
export class ListerParkingComponent implements OnInit {

    rechercheForm: FormGroup;
    parkings: any[] = [];
    paysItems: ReadPaysDTO[];
    villeItems: any[];
    loading: boolean = false;
    @ViewChild('filter') filter!: ElementRef;

    subscription: Subscription;
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder,
        private paysService: PaysService,
        private parkingService: ParkingService,
        private messageService: MessageService,
        private router: Router) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/
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

    ngOnInit() {

        this.rechercheForm = this.formBuilder.group({
            pays: ['', [Validators.required]],
            ville: ['', [Validators.required]],
          });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }
    
    afficherParking() {

        this.rechercheForm.markAllAsTouched();
        if(this.rechercheForm.valid) {
            //this.blockUI.start('Opération en cours ...');
            this.loading = true;
            this.parkingService.apiParkingsPaysPaysIdVilleNomVilleGet(
                this.rechercheForm.value.pays.payS_Id,
                this.rechercheForm.value.ville).subscribe(
                {
                    next : (result) => {
                        console.log(result);
                        this.parkings = result;
                        this.loading  = false;
                    },
                    error : (err) => {
                        this.loading  = false;
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            );
        }
    }

    ajouterEmploye(idParking: number, parkingNom: string, parkingNumero: string, parkingNomRue: string) {
        this.router.navigateByUrl('/admin/gestion-employe/ajouter-employe', { state: { 'idParking' : idParking, 'detail': 
            parkingNom + ', ' + parkingNumero + ' ' + parkingNomRue + ' - ' + this.rechercheForm.value.pays.payS_Nom
         } });
    }

    listerEmploye(idParking: number, parkingNom: string, parkingNumero: string, parkingNomRue: string) {
        this.router.navigateByUrl('/admin/gestion-employe/lister-employe', { state: { 'idParking' : idParking, 'detail': 
            parkingNom + ', ' + parkingNumero + ' ' + parkingNomRue + ' - ' + this.rechercheForm.value.pays.payS_Nom
         } });
    }

    ajouterParking() {
          this.router.navigate(['/admin/gestion-parking/ajouter-parking']);
    }

    onPaysChange(event) {
        console.log(event);
        this.villeItems = [];
        this.parkingService.apiParkingsVillesPaysPaysIdGet(event.value.payS_Id).subscribe(
            {
                next : (result) => {
                    console.log(result);
                    this.villeItems = result;
                    this.afficherParking();
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );        
    }

    onVilleChange(event) {

        this.parkings = [];
        this.afficherParking();
    }
}
