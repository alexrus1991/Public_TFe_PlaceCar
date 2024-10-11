import { Component, OnInit, OnDestroy } from '@angular/core';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { Product } from '../../api/product';
import { ProductService } from '../../service/product.service';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { AuthService } from '../../service/auth.service';
import { Route, Router } from '@angular/router';
import { ClientColtrollerService, EmplyeeService, FactureService, ParkingService, PaysService, PlaceService, ReadEmpWorkInDTO, ReadPaysDTO, ReservationService } from '../../api/generated';
import { CountryService } from '../../service/country.service';
import '../../../date.extension';

interface expandedRows {//pour gérer l’expansion des lignes dans les tableaux de données.
    [key: string]: boolean;
}
const MONTHS = ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'];

@Component({
    templateUrl: './dashboard.component.html',
    providers: [MessageService, ConfirmationService]
})
export class DashboardComponent implements OnInit, OnDestroy {

    items!: MenuItem[];

    products: Product[] = [];

    chartData: any;
    reservationMoisData: any;
    transactionMoisData: any;

    chartOptions: any;

    subscription!: Subscription;

    expandedRows: expandedRows = {};

    isExpanded: boolean = false;
    nombreClient = 0;
    nombreEmploye = 0;
    nombreParking = 0;
    nombreReservationMois = 0;
    nombreReservationJour = 0;
    parkings: any[] = [];
    countries: any[] = [];
    idParking = 0;
    dateDebut= new Date();
    pieData: any;
    pieOptions: any;
    numeroEtageSelectione = 0;
    reservationsStatisticsDepartArriveParkingParkingIdData: any;
    facturesStatisticsParkingParkingIdData: any;
    placesParkingOccupationParkingParkingIdData: any;
    placesParkingOccupationParkingParkingidEtageEtageNumeroData: any;
    numeroEtageItems: any[] = [];

    constructor(private productService: ProductService, 
        public layoutService: LayoutService,
        public authService: AuthService,
        private clientColtrollerService: ClientColtrollerService,
        private emplyeeService: EmplyeeService,
        private paysService: PaysService,
        private countryService: CountryService,
        private parkingService: ParkingService,
        private reservationService: ReservationService,
        private placeService: PlaceService,
        private factureService: FactureService,
        private router: Router
    ) {

        console.log(this.authService.getUser());
        if(!this.authService.isLoggedIn() 
            || (this.authService.isLoggedIn() && this.authService.getUser()['role']?.toLowerCase() === 'client')) {
            this.router.navigateByUrl('/gestion-reservations/creer-reservation');

            return;
        }

        this.layoutService.state.staticMenuDesktopInactive = true;
        this.layoutService.onMenuToggle();

        this.subscription = this.layoutService.configUpdate$
        .pipe(debounceTime(25))
        .subscribe((config) => {
            this.initChart();
        });
        
        this.countryService.getCountries().then(countries => {
            this.countries = countries;
        });
    }

    ngOnInit() {

        if(!this.authService.isLoggedIn()) {
            return;
        }

        //this.productService.getProductsWithOrdersSmall().then(data => this.products = data);

        if(this.authService.getUser()['role'].toLowerCase() === 'employee') {

            this.parkingService.apiParkingsEmployeeEmployeeIdGet(this.authService.getUser()['nameIdentifier']).subscribe({
                next : (result: ReadEmpWorkInDTO) => {
                    //this.blockUI.stop();
                    this.idParking = result['parK_Id'];
    
                    this.emplyeeService.apiEmployeesCountNombreTotalEmployeesParkingGet(this.idParking).subscribe({
                        next: (nombreEmploye) => {
                            this.nombreEmploye = nombreEmploye;
                        }
                    });  

                    this.reservationService.apiReservationsCountNombeReservationsTotalDuMoisParkingParkingidGet(this.idParking,
                        (new Date()as any).toISO()
                    ).subscribe({
                        next: (nombreReservationMois) => {
                            this.nombreReservationMois = nombreReservationMois;
                        }
                    });  

                    this.reservationService.apiReservationsCountNombeReservationsParkingParkingidGet(this.idParking,
                        (new Date() as any).toISO()
                    ).subscribe({
                        next: (nombreReservationJour) => {
                            this.nombreReservationJour = nombreReservationJour;
                        }
                    }); 
                    
                    this.initChart();
                }
            });            
        }
        else if(this.authService.getUser()['role'].toLowerCase() === 'admin' || 
        this.authService.getUser()['role'].toLowerCase() === 'superadmin') {
            this.clientColtrollerService.apiClientsCountGet().subscribe({
                next: (nombreClient) => {
                    this.nombreClient = nombreClient;
                }
            });
            this.emplyeeService.apiEmployeesCountNombreTotalEmployeesGet().subscribe({
                next: (nombreEmploye) => {
                    this.nombreEmploye = nombreEmploye;
                }
            });
            this.emplyeeService.apiEmployeesCountNombreTotalParkingsGet().subscribe({
                next: (nombreParking) => {
                    this.nombreParking = nombreParking;
                }
            });
            this.emplyeeService.apiEmployeesStatReservationMoisGet().subscribe({
                next: (result) => {
                    console.log('apiEmployeesStatReservationMoisGet');
                    console.log(result);
                    console.log("----Zorrooooo-----")

                    let labels = [];
                    let reservationNombreData = [];
                    let totalDureeData = [];

                    for (let index = 0; index < result?.length; index++) {
                        const element = result[index];
                        
                        labels.push(MONTHS[element['month'] - 1]);
                        reservationNombreData.push(element['reservationNombre']);
                        totalDureeData.push(element['totalDuree']);
                    }

                    const documentStyle = getComputedStyle(document.documentElement);
                    this.reservationMoisData = {
                        labels: labels,
                        datasets: [
                            {
                                label: 'Nombre de réservation',
                                data: reservationNombreData,
                                fill: false,
                                backgroundColor: documentStyle.getPropertyValue('--bluegray-700'),
                                borderColor: documentStyle.getPropertyValue('--bluegray-700'),
                                tension: .4
                            },
                            {
                                label: 'Durée totale',
                                data: totalDureeData,
                                fill: false,
                                backgroundColor: documentStyle.getPropertyValue('--green-600'),
                                borderColor: documentStyle.getPropertyValue('--green-600'),
                                tension: .4
                            }
                        ]
                    };
                }
            });

            this.emplyeeService.apiEmployeesStatTransactionMoisGet().subscribe({
                next: (result) => {
                    console.log('apiEmployeesStatTransactionMoisGet');
                    console.log(result);

                    let labels = [];
                    let transactionNombreData = [];
                    let totalSommeData = [];

                    for (let index = 0; index < result?.length; index++) {
                        const element = result[index];
                        
                        labels.push(MONTHS[element['month'] - 1]);
                        transactionNombreData.push(element['transactionNombre']);
                        totalSommeData.push(element['totalSomme']);
                    }

                    const documentStyle = getComputedStyle(document.documentElement);
                    this.transactionMoisData = {
                        labels: labels,
                        datasets: [
                            {
                                label: 'Nombre de transaction',
                                data: transactionNombreData,
                                fill: false,
                                backgroundColor: documentStyle.getPropertyValue('--bluegray-700'),
                                borderColor: documentStyle.getPropertyValue('--bluegray-700'),
                                tension: .4
                            },
                            {
                                label: 'Somme totale',
                                data: totalSommeData,
                                fill: false,
                                backgroundColor: documentStyle.getPropertyValue('--green-600'),
                                borderColor: documentStyle.getPropertyValue('--green-600'),
                                tension: .4
                            }
                        ]
                    };
                }
            });

            this.parkingService.apiParkingsToutLesParkingsGet().subscribe({
                next : (result) => {
                    this.parkings = [];
                    
                    for (let index = 0; index < result.length; index++) {
                        const element = result[index];
                        
                        let array = this.countries.filter((c) => c.name === element['payS_Nom']);
                        if(array && array.length > 0) {
                            element['code'] = array[0].code;
                        }

                        this.reservationService.apiReservationsParkingParkingIdNonClotureGet(element['parK_Id']).subscribe({                 
                            next: (reservations) => {
                                element['reservations'] = reservations;
                            }
                        });

                        this.parkings.push(element);
          
                    }             
                },
            });
    
        }
    }

    initChart() {
        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        if(this.authService.getUser()['role'].toLowerCase() === 'employee') {

            console.log('date');
            console.log(  this.dateDebut.toLocaleDateString("fr-be", {year:"numeric",month:"2-digit", day:"2-digit"}));
            console.log((this.dateDebut as any).toISO());

            this.reservationService.apiReservationsStatisticsDepartArriveParkingParkingidGet(this.idParking,
                (this.dateDebut as any).toISO()
            ).subscribe({
                next: (result) => {
                    console.log('apiReservationsStatisticsDepartArriveParkingParkingidGet');
                    console.log(result);

                    this.reservationsStatisticsDepartArriveParkingParkingIdData = {
                        labels: ['Réservation commence', 'Réservation termine'],
                        datasets: [
                            {
                                data: [result.reservationsCommencent, result.reservationsTerminent],
                                backgroundColor: [
                                    documentStyle.getPropertyValue('--indigo-500'),
                                    documentStyle.getPropertyValue('--purple-500'),
                                ],
                                hoverBackgroundColor: [
                                    documentStyle.getPropertyValue('--indigo-400'),
                                    documentStyle.getPropertyValue('--purple-400'),
                                ]
                            }]
                    };
                }
            });  

            this.factureService.apiFacturesStatisticsParkingParkingIdGet(this.idParking,
               (this.dateDebut as any).toISO()
            ).subscribe({
                next: (result) => {
                    console.log('apiReservationsStatisticsDepartArriveParkingParkingidGet');
                    console.log(result);

                    this.facturesStatisticsParkingParkingIdData = {
                        labels: ['Facture payée', 'Facture non payé'],
                        datasets: [
                            {
                                data: [result.factPayee, result.factNonPaye],
                                backgroundColor: [
                                    documentStyle.getPropertyValue('--yellow-500'),
                                    documentStyle.getPropertyValue('--orange-500')
                                ],
                                hoverBackgroundColor: [
                                    documentStyle.getPropertyValue('--yellow-400'),
                                    documentStyle.getPropertyValue('--orange-400')
                                ]
                            }]
                    };
                }
            });  

            this.placeService.apiPlacesParkingOccupationParkingParkingidGet(this.idParking,
                (this.dateDebut as any).toISO()
            ).subscribe({
                next: (result) => {
                    console.log('apiPlacesParkingOccupationParkingParkingidGet');
                    console.log(result);

                    this.placesParkingOccupationParkingParkingIdData = {
                        labels: ['Occupé', 'Libre'],
                        datasets: [
                            {
                                data: [result.occupees, result.libres],
                                backgroundColor: [
                                    documentStyle.getPropertyValue('--red-500'),
                                    documentStyle.getPropertyValue('--green-500')
                                ],
                                hoverBackgroundColor: [
                                    documentStyle.getPropertyValue('--red-400'),
                                    documentStyle.getPropertyValue('--green-400')
                                ]
                            }]
                    };
                }
            });  
            
            this.parkingService.apiParkingsIdGet(this.idParking).subscribe({
                next : (result) => {
                    console.log(result);
                    this.numeroEtageItems = [];
                    for (let index = 1; index <= result.parK_NbEtages; index++) {

                        if(index === 1) {
                            this.onSelectionNumeroEtageChange({
                                'value': 1
                            })
                        }
                        this.numeroEtageItems.push(index);
                    }
                    console.log( this.numeroEtageItems);
                }
            });
            
        }

        this.chartData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [
                {
                    label: 'First Dataset',
                    data: [65, 59, 80, 81, 56, 55, 40],
                    fill: false,
                    backgroundColor: documentStyle.getPropertyValue('--bluegray-700'),
                    borderColor: documentStyle.getPropertyValue('--bluegray-700'),
                    tension: .4
                },
                {
                    label: 'Second Dataset',
                    data: [28, 48, 40, 19, 86, 27, 90],
                    fill: false,
                    backgroundColor: documentStyle.getPropertyValue('--green-600'),
                    borderColor: documentStyle.getPropertyValue('--green-600'),
                    tension: .4
                }
            ]
        };

        this.chartOptions = {
            plugins: {
                legend: {
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                }
            }
        };

        this.pieData = {
            labels: ['A', 'B', 'C'],
            datasets: [
                {
                    data: [540, 325, 702],
                    backgroundColor: [
                        documentStyle.getPropertyValue('--indigo-500'),
                        documentStyle.getPropertyValue('--purple-500'),
                        documentStyle.getPropertyValue('--teal-500')
                    ],
                    hoverBackgroundColor: [
                        documentStyle.getPropertyValue('--indigo-400'),
                        documentStyle.getPropertyValue('--purple-400'),
                        documentStyle.getPropertyValue('--teal-400')
                    ]
                }]
        };

        this.pieOptions = {
            plugins: {
                legend: {
                    labels: {
                        usePointStyle: true,
                        color: textColor
                    }
                }
            }
        };
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    expandAll() {
        if (!this.isExpanded) {
            this.products.forEach(product => product && product.name ? this.expandedRows[product.name] = true : '');

        } else {
            this.expandedRows = {};
        }
        this.isExpanded = !this.isExpanded;
    }

    onDateDebutChange(selectedDate: any) {
        console.log(event);
        this.dateDebut = selectedDate;
       // this.dateDebut.setDate(this.dateDebut.getDate() + 1)
        this.dateDebut.setHours(1);
        //this.dateDebut = new Date(selectedDate.getTime() + (24 * 60 *60 *1000));
        console.log(this.dateDebut);

        this.initChart();
    }

    onSelectionNumeroEtageChange(event: any) {
        console.log(event);
        this.numeroEtageSelectione = event.value;

        const documentStyle = getComputedStyle(document.documentElement);

        this.placeService.apiPlacesParkingOccupationParkingParkingidEtageEtageNumeroGet(this.idParking, this.numeroEtageSelectione, (this.dateDebut as any).toISO()).subscribe({
            next: (result) => {
                console.log('apiPlacesParkingOccupationParkingParkingidEtageEtageNumeroGet');
                console.log(result);

                this.placesParkingOccupationParkingParkingidEtageEtageNumeroData = {
                    labels: ['Places réservés', 'Places non réservées'],
                    datasets: [
                        {
                            data: [result['placesReservees'], result['placesNonReservees']],
                            backgroundColor: [
                                documentStyle.getPropertyValue('--green-600'),
                                documentStyle.getPropertyValue('--pink-500')
                            ],
                            hoverBackgroundColor: [
                                documentStyle.getPropertyValue('--green-600'),
                                documentStyle.getPropertyValue('--pink-400')
                            ]
                        }]
                };
            }
        });  
    }
}
