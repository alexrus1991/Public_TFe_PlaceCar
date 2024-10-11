import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { EmplyeeService, ParkingService, PaysService, PlaceService, PlaceStatusDto, ReadEmpWorkInDTO, ReadParkingDTO, ReadPaysDTO } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';
import '../../../../../date.extension';

class Parking {
  constructor(public numeroEtage:number, public numeroPlace:number, public parkingStatus:number)
{

}
}
@Component({
    templateUrl: './detail-parking.component.html',
    styleUrls: ['./detail-parking.component.css'],
    providers: [ConfirmationService, MessageService]
})
export class DetailParkingComponent implements OnInit {

   placeForm: FormGroup;
    isSelected: boolean = false;
    status = [
      { status: 'Occupé' },
      { status: 'Disponible' },      
      { status: 'Réservé' },
      { status: 'Sélectionné' },
      // Ajoutez plus d'emplacements selon les besoins
    ];
  parkingsFloor1: Parking[] = [];
  parkingsFloor2: Parking[] = [];
  selectedSpotFloor1: number | null = null;
  selectedSpotFloor2: number | null = null;
  parkingId: number | null = null;
  etageSelectione: number | null = null;
  selectedSpotFloor: number | null = null;
  placeStatusList: PlaceStatusDto[] = [];
  parkingsFloorList: Parking[] = [];
  etageItems: any[] = [];
  today = new Date();

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
        private placeService: PlaceService,
        private router: Router) {
    }

    ngOnInit() {

        console.log(this.authService.getUser());

        this.blockUI.start('Chargement en cours ...');

        this.parkingService.apiParkingsEmployeeEmployeeIdGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next : (result: ReadEmpWorkInDTO) => {
                this.blockUI.stop();
                console.log(result);

                this.parkingId = result.parK_Id;

                this.parkingService.apiParkingsIdGet(this.parkingId).subscribe(
                  {
                      next : (result: ReadParkingDTO) => {   
                          console.log('ReadParkingDTO');
                          console.log(result);
                          this.etageItems = [];
                          if(result) {
                              for(var i = 0; i < result.parK_NbEtages; i++) {
                                  this.etageItems.push(i + 1); 
                              }
                          }
                      },
                      error : (err) => {
                          console.log(err);
                          //this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                      }
                  }
              ); 


                /*this.parkingsFloor1.push(new Parking(1,0))
       this.parkingsFloor1.push(new Parking(2,0))
       this.parkingsFloor1.push(new Parking(3,1))
       this.parkingsFloor1.push(new Parking(4,1))
       this.parkingsFloor1.push(new Parking(5,0))
       this.parkingsFloor1.push(new Parking(6,0))
       this.parkingsFloor1.push(new Parking(7,0))
       this.parkingsFloor1.push(new Parking(8,0))
       this.parkingsFloor1.push(new Parking(9,0))
       this.parkingsFloor1.push(new Parking(10,0))
       this.parkingsFloor1.push(new Parking(11,0))
       this.parkingsFloor1.push(new Parking(12,0))
       this.parkingsFloor1.push(new Parking(13,0))
       this.parkingsFloor1.push(new Parking(14,0))
       this.parkingsFloor1.push(new Parking(15,0))
       this.parkingsFloor1.push(new Parking(16,2))
       this.parkingsFloor1.push(new Parking(17,0))
       this.parkingsFloor1.push(new Parking(18,0))
      // this.parkingsFloor1.push(new Parking(19,0))
       //this.parkingsFloor1.push(new Parking(20,2))
       //this.parkingsFloor1.push(new Parking(21,0))
  //Pour la première ligne
  this.parkingsFloor2.push(new Parking(1,0))
  this.parkingsFloor2.push(new Parking(2,0))
  this.parkingsFloor2.push(new Parking(3,1))
  this.parkingsFloor2.push(new Parking(4,1))
  this.parkingsFloor2.push(new Parking(5,0))
  this.parkingsFloor2.push(new Parking(6,0))
  this.parkingsFloor2.push(new Parking(7,0))
  this.parkingsFloor2.push(new Parking(8,0))
  this.parkingsFloor2.push(new Parking(9,0))
  this.parkingsFloor2.push(new Parking(10,0))
  this.parkingsFloor2.push(new Parking(11,1))
  this.parkingsFloor2.push(new Parking(12,0))
  this.parkingsFloor2.push(new Parking(13,0))
  this.parkingsFloor2.push(new Parking(14,0))
  this.parkingsFloor2.push(new Parking(15,0))
  this.parkingsFloor2.push(new Parking(16,2))
  this.parkingsFloor2.push(new Parking(17,0))
  this.parkingsFloor2.push(new Parking(18,1))
  //this.parkingsFloor2.push(new Parking(19,0))
  //this.parkingsFloor2.push(new Parking(20,0))
 // this.parkingsFloor2.push(new Parking(21,0))

 */
            },   
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });


        
        this.placeForm = this.formBuilder.group({
            dateSelection: ['', [Validators.required]],
            etage: ['', [Validators.required]],
          });

          this.placeForm.patchValue({
            'dateSelection' : this.today
          });

       /*this.employeeService.apiEmployeesDuGroupeGet().subscribe({
            next : (result) => {
                this.blockUI.stop();
                console.log(result);
            },
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });*/
        
        /*.subscribe({
            next : (result) => {
                this.blockUI.stop();
                console.log(result);
              
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });*/
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

    onSelectionEtageChange(event) {
      console.log(event);

      this.etageSelectione = event.value;
      this.selectedSpotFloor = null;
      this.listerPlace();
   }

   onSelectionDateChange(event) {
    console.log(event);
    this.listerPlace();
 
 }

   listerPlace() {

    if(this.placeForm.valid) {

        console.log(this.placeForm.value);

        this.placeService.apiPlacesParkingParkingParkingidEtageEtageIdGet(this.placeForm.value.etage, this.parkingId, (this.placeForm.value.dateSelection as any).toISO()).subscribe(
            {
                next : (result: PlaceStatusDto[]) => {       
                    console.log(result);    
                    this.placeStatusList = result;
                    this.parkingsFloorList = [];
                    
                    for(var i = 0; i < this.placeStatusList.length; i++) {
                        const element = this.placeStatusList[i];
                        this.parkingsFloorList.push(new Parking(this.etageSelectione, element.numeroPlace, element.status));
                    }
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );
    }
    /* */
  }


    getStatus(index: number) {
        console.log(`status : ${index} - ${this.status[index].status}`);
     return this.status[index].status;
    }
    selectSpot(index: number, status: number) {

      /*if(status === 1 || status === 2) {
          return;
      }
      console.log(this.selectedSpotFloor)
      if (this.selectedSpotFloor === index) {
          this.selectedSpotFloor = null;
      } else {

          this.selectedSpotFloor = index;
          //this.selectedSpotFloor = null;
      }*/
   }
}
