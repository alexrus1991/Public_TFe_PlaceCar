import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { EmplyeeService, ParkingService, PaysService, ReadEmpWorkInDTO, ReadPaysDTO } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './lister-employe.component.html',
    providers: [ConfirmationService, MessageService]
})
export class ListerEmployeComponent implements OnInit {

    employes: any[] = [];
    parkingItems: any[];
    loading: boolean = false;
    idParking = 0;
    @ViewChild('filter') filter!: ElementRef;

    subscription: Subscription;
    @BlockUI() blockUI: NgBlockUI;

    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder,
        private paysService: PaysService,
        private parkingService: ParkingService,
        private messageService: MessageService,
        private employeeService: EmplyeeService,
        private confirmationService: ConfirmationService, 
        private authService: AuthService,
        private router: Router) {
    }

    ngOnInit() {

        console.log(this.authService.getUser());

        this.blockUI.start('Chargement en cours ...');

        this.parkingService.apiParkingsEmployeeEmployeeIdGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next : (result: ReadEmpWorkInDTO) => {
                //this.blockUI.stop();
                this.idParking = result['parK_Id'];

                this.employeeService.apiEmployeesParkingParkingIdGet(result.parK_Id).subscribe({
                    next : (result) => {
                        this.blockUI.stop();
                        console.log(result);

                        this.employes = [];
                        for (let index = 0; index < result.length; index++) {
                            const element = result[index];
                            if(element['emp_Pers_Id'] != this.authService.getUser()['nameIdentifier']) {
                                element['perS_DateNaissance'] = element['perS_DateNaissance'].substring(0, 10);
                                this.employes.push(element);
                            }
                        }
                    },
                    error : (err) => {
                        console.log(err);
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                });
            },   
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });

    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }

    rechercher(idParking: number) {
       /* if(this.rechercheForm.valid) {
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
        } */
    }
}
