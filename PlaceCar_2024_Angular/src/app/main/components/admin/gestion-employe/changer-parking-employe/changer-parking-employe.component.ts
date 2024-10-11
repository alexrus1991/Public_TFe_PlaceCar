import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { EmplyeeService, ParkingService, PaysService, ReadPaysDTO, UserService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './changer-parking-employe.component.html',
    providers: [MessageService]
})
export class ChangerParkingEmployeComponent implements OnInit {

    utilisateurForm: FormGroup;
    idEmploye: number;
    paysItems: ReadPaysDTO[];
    villeItems: any[];
    parkingItems: any[];
    nomEmploye = '';
    prenomEmploye = '';
    
    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private paysService: PaysService,
        private parkingService: ParkingService,
        private userService: UserService,
        private emplyeeService: EmplyeeService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute,
        private authService: AuthService,
        private router: Router
    ) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/ 

            let state = this.router.getCurrentNavigation()?.extras?.state;
            if(state) {
                this.idEmploye = state['idEmploye'];
                this.nomEmploye = state['nomEmploye'];
                this.prenomEmploye = state['prenomEmploye'];

            }

           if(this.idEmploye > 0) {
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

    ngOnInit() {

        this.utilisateurForm = this.formBuilder.group({
            pays: ['', [Validators.required]],
            ville: ['', [Validators.required]],
            parking: ['', [Validators.required]],
          });
    }

    valider() {
      
        this.utilisateurForm.markAllAsTouched();
        if(this.utilisateurForm.valid && this.idEmploye > 0) {

            this.blockUI.start('Opération en cours ...');
            this.emplyeeService.apiEmployeesParkingParkingIdChangementPut(this.utilisateurForm.value.parking.parK_Id, this.idEmploye).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Parking changé avec succès' });
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

    onPaysChange(event) {
        console.log('event :' + event);
        console.log(event.value);

        if(event.value?.payS_Id > 0) {
            this.villeItems = [];
            this.villeItems.push("Bruxelles", "Paris");
        }
    }

    onVilleChange(event) {
        console.log('event :' + event);
        console.log(event.value);

        this.parkingItems = [];
        this.parkingService.apiParkingsPaysPaysIdVilleNomVilleGet(
            this.utilisateurForm.value.pays.payS_Id,
            this.utilisateurForm.value.ville).subscribe(
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
}
