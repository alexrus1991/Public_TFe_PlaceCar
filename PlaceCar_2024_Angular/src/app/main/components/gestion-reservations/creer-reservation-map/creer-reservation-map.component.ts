import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { Product } from '../../../api/product';
import { ProductService } from '../../../service/product.service';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { Router } from '@angular/router';
import { CountryService } from '../../../service/country.service';
import { FormuleOptionDTO, FormulePrixService, GeoapifyService, ParkingDto, ParkingService, PaysService, PlaceService, PlaceStatusDto, ReadForulesParkingDTO, ReadParkingDTO, ReadPaysDTO, ReadPlaceLibDTO, ReadTypeFormDTO, ReservationService } from '../../../api/generated';
import { AuthService } from '../../../service/auth.service';
import * as L from 'leaflet';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OverlayPanel } from 'primeng/overlaypanel';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Title } from 'chart.js';
import { aD } from '@fullcalendar/core/internal-common';
import '../../../../date.extension';
import '../../../../string.extension';

class Parking {
    constructor(public numeroEtage:number, public numeroPlace:number, public parkingStatus:number)
  {

  }
}
@Component({
    templateUrl: './creer-reservation-map.component.html',
    styleUrls: ['./creer-reservation-map.component.css'],
    providers: [MessageService]
})
export class CreerReservationMapComponent implements OnInit, OnDestroy, AfterViewInit {
    LibelleFormules : ReadForulesParkingDTO[]=[];
    dateForm: FormGroup;//gérer les entrées utilisateur
    utilisateurForm: FormGroup;//gérer les entrées utilisateur
    placeLibreForm: FormGroup;//gérer les entrées utilisateur
    pays: any;
    today = new Date();
    parkings: any[] = [];// Des tableaux contenant les informations des places de parking disponibles
    filteredParkings: any[] = [];
    parking: any;
    address: any;
    rechercheAdresse: any = 'Recherche';
    dateDebut: Date;
    afficherDialogConnexion = false;
    formulePrixList: ReadForulesParkingDTO[] = [];
    placeLibreList: ReadPlaceLibDTO[] = [];
    formuleOptionList: FormuleOptionDTO[] = [];
    afficherConfirmationReservationDialog = false;
    heureItems: any[] = [];
    activeIndex: number = 0;
    dateDebutReservation: Date;
    dateFinReservation: Date;
    etageItems: any[] = [];
    parkingsFloor: Parking[] = [];//Des listes de places de parking organisées par étage, représentées par la classe Parking
    parkingsFloorList: Parking[] = [];
    isSelected: boolean = false;
    status = [
      { status: 'Occupé' },
      { status: 'Disponible' },
      { status: 'Réservé' },
      { status: 'Sélectionné' },
      // Ajoutez plus d'emplacements selon les besoins
    ];
    selectedSpotFloor: number | null = null;
    etageSelectione: number | null = null;
    selectedFormuleDePrix = null;
    placeStatusList: PlaceStatusDto[] = [];
    //@ViewChild('panel', {static:true}) overlayPanel: OverlayPanel;
    @ViewChild('op', {static : true}) oPanel: OverlayPanel;
    //private oPanelTarget: any;
    @BlockUI() blockUI: NgBlockUI;
    
    private markers: any[] = [];
    @ViewChild('mapContainer') private mapContainer: ElementRef;
    private map: any;

    //Un icône personnalisé pour les marqueurs de la carte
    defaultIcon = L.icon({
        iconUrl: 'assets/demo/images/leaflet/marker-icon-new.png',
        shadowUrl: 'assets/demo/images/leaflet/marker-shadow.png',
        //iconSize: [24,36],
        iconSize: [32,32],
        iconAnchor: [12,36]
      });

    constructor(public layoutService: LayoutService, 
        public router: Router,
        private formBuilder: FormBuilder, 
        private countryService: CountryService,
        private paysService: PaysService,
        private parkingService: ParkingService,
        private authService: AuthService,
        private geoapifyService: GeoapifyService,
        private placeService: PlaceService,
        private formulePrixService: FormulePrixService,
        private reservationService: ReservationService,
        private messageService: MessageService,
        private http: HttpClient, 
    ) { 

        //this.blockUI.start('Initialisation en cours ...');

        for(var i = 0; i < 24; i++) {
            this.heureItems.push(("0" + i).slice(-2) + ":00");
        }//génère une liste d'heures pour l'interface utilisateur.

        L.Marker.prototype.options.icon = this.defaultIcon;
        let state = this.router.getCurrentNavigation()?.extras?.state;
        if(state) {
            this.pays = this.router.getCurrentNavigation().extras?.state['pays'];
        }//configure une icône personnalisée pour les marqueurs sur une carte interactive

        if(!this.pays) {
            //gère la récupération des informations de navigation
            this.pays = {
                payS_Id: 1, 
                payS_Nom: 'Belgique'
            };
        }

        console.log(this.pays);
    }


    ngOnInit() {//une méthode de cycle de vie dans les composants

        this.utilisateurForm = this.formBuilder.group({
            login: ['', [Validators.required]],
            password: ['', [Validators.required]],
          });//capturer les informations de connexion d'un utilisateur

        this.dateForm = this.formBuilder.group({
            dateDebut: ['', [Validators.required]],
            heureDebut: ['12:00'],
            dateFin: [''],
            heureFin: [],
          });//gère les informations sur les dates et heures de début et de fin d'une réservation
        this.placeLibreForm = this.formBuilder.group({
            etage: [],
          });

        if(this.pays) {
            this.rechercheAdresse = this.pays.payS_Nom;
            this.rechergerParkingByPays(this.pays);
        }    
    }

    ngOnDestroy() {
        //this.map.remove();
    }
    
    ngAfterViewInit(): void {//garantit que tout est initialisé correctement une fois que la vue est prête
// crée une nouvelle carte Leaflet dans l'élément HTML référencé par this.mapContainer.nativeElement
        this.map = L.map(this.mapContainer.nativeElement, {
            center: [ 39.8282, -98.5795 ],
            zoom: 5,
            //zoomControl: false
         });
         const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {//position sur carte USA centre
            maxZoom: 18,
            minZoom: 5,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
          });
      
        tiles.addTo(this.map);//ajoute la couche de tuiles (OpenStreetMap) à la carte pour qu'elle soit visible à l'écran.

        //this.map.setView([ 39.8282, -98.5795 ], this.map.getZoom(), {animate: true});
    }

    onAddressChange(event: any) {//filtrant une liste de parkings en fonction de la saisie de l'utilisateur
        const query = event.target.value;
        if(query?.length >= 3) {

            this.filteredParkings = [];

            const filtered: any[] = [];//tableau temporaire filtered est créé pour stocker les parking
            // tslint:disable-next-line:prefer-for-of
            for (let i = 0; i < this.parkings.length; i++) {
                const p = this.parkings[i];
                if (p.adrS_NomRue?.toLowerCase().indexOf(query.toLowerCase()) == 0
                    || p.adrS_Ville?.toLowerCase().indexOf(query.toLowerCase()) == 0
                    || p.adrS_NomRue?.toLowerCase().indexOf(query.toLowerCase()) == 0
                    || p.payS_Nom?.toLowerCase().indexOf(query.toLowerCase()) == 0) {
                    filtered.push(p);
                }
            }
            this.filteredParkings = filtered;
        }
    }

    onParkingClick(parking: any) {
        if(parking) {

            this.markers.forEach((item, index) => {//tableau qui contient les marqueurs de parking
                if (item.options.parking.adrS_Id === parking.adrS_Id) {//compare l'identifiant du parking cliqué (parking.adrS_Id) avec celui du parking associé au marqueur
                    //chaque marqueur possède une option parking qui contient des informations sur le parking correspondant
                    let m = this.markers[index];
                    m.remove();
                    this.markers.splice(index, 1);
                }
            });


            console.log(parking);//enlever la selection du parking ds la liste
            for (let index = 0; index < this.parkings.length; index++) {
                const element = this.parkings[index];
                element['selected'] = false;
            }

            //this.blockUI.start('Recherche en cours ...');
            this.parking = parking;
            this.parking['selected'] = true;//prend  la nouvelle selection selection du parking ds la liste
            //this.removeAllMarkers();

            //this.filteredParkings = [];
            //this.filteredParkings.push(this.parking);
    
            //let address = parking.adrS_NomRue + ' ' + parking.adrS_Numero + ', ' + parking.adrS_Ville + ' ' + parking.payS_Nom;
            this.addMarker(parking['feature'].properties.lat, parking['feature'].properties.lon, parking, (marker: any) => {
                console.log(marker);
                marker.openPopup();
                this.map.setView(marker._latlng, 18);
            });

            this.etageItems = [];
            for(var i = 0; i < parking.parK_NbEtages; i++) {
                this.etageItems.push(i + 1); 
            }
        }
    }

    afficherTousLesParkings(liste: any[]) {//afficher une liste de parkings sur une carte

        //this.blockUI.start('Recherche en cours ...');

        this.parking = null;//Réinitialise l'objet parking à null
        this.parkings = [];// Vide la liste des parkings
        this.filteredParkings = [];//Vide également la liste des parkings filtrés
        this.removeAllMarkers();//Supprime tous les marqueurs précédentes

        for (let index = 0; index < liste.length; index++) {
            const parking = liste[index];
            //this.filteredParkings.push(parking);
            
            console.log(parking);

            let address = parking.adrS_NomRue + ' ' + parking.adrS_Numero + ', ' + parking.adrS_Ville + ' ' + parking.payS_Nom;
            this.geocodeAddress(address, (result: any) => {
                    // l'adresse pour obtenir les coordonnées géographiques 
                parking['feature'] = result;//géocodage est ajouté au parking 

                this.parkings.push(parking);
                this.filteredParkings.push(parking);

                this.addMarker(result.properties.lat, result.properties.lon, parking);
         //placer un marqueur sur la carte à l'emplacement correspondant aux coordonnées géographiques 
                if(index == liste.length - 1) {
                    this.blockUI.stop();
                }
            });
        }
    }

    geocodeAddress(address: any, callback: any) {
//responsable de géocoder une adresse pour obtenir ses coordonnées géographiques
        this.geoapifyService.addressGet(address).subscribe({//Appele vers API Controller
            next: (result) => {
                let featureCollection = JSON.parse(result);

                if (featureCollection.features.length === 0) {
                    return;
                }

                const foundAddress = featureCollection.features[0];
                console.log(foundAddress);
                callback(foundAddress);
            },
            error: (err) => {

            }
        });
    }

    addMarker(lat: number, lon: number, parking: any, callback: any = null) {
        //let marker = L.marker(new L.LatLng(lat, lon), options);

        let address = parking.adrS_NomRue + ' ' + parking.adrS_Numero + ', ' + parking.adrS_Ville + ' ' + parking.payS_Nom;
        let options: any = {
            /*icon: new L.DivIcon({
                className: 'my-div-icon',
                html: '<div class="my-div-span" ><p style="color: black; background: whitesmoke">' + parking.parK_Nom + '</p>' +
                '<img src="assets/demo/images/leaflet/marker-icon-new.png" alt="' + address + '" title="' + address + '"></div>'
            }),*/
            icon: new L.DivIcon({
                className: 'my-div-icon',
                html: '<div class="my-div-span" ><img src="assets/demo/images/leaflet/marker-icon-new.png" alt="' + address + '" title="' + address + '"></div>'
            }),
            'title': address,
            'alt': address,
            'parking': parking        
        };
        let marker = L.marker(new L.LatLng(lat, lon), options)
                        .bindPopup('<strong>' + parking.parK_Nom  + '</strong> <br> ' + address, {autoClose: false, autoPan: false})
                        .on('click', (event) => {
                            this.onParkingClick(event.sourceTarget.options.parking);
                        });
                 
        this.markers.push(marker);
        console.log('Markers length: ' + this.markers.length);

        var group = L.featureGroup(this.markers).addTo(this.map);
        marker.openPopup();
        //.bindPopup('<strong>' + parking.parK_Nom  + '</strong> <br> ' + address, {autoClose: false, autoPan: false})
        //.openPopup()
        /*.on('click', (event) => {
            this.onParkingClick(event.sourceTarget.options.parking);
        });*/

        if(callback == null) {
            this.map.fitBounds(group.getBounds(), {
                maxZoom: 10
            });
        }
        else {
            callback(marker);
        }    
    }

    removeAllMarkers() {
        this.map.closePopup();
        for (let index = 0; index < this.markers.length; index++) {
            const marker = this.markers[index];
            if(marker) {
                marker.remove();
            }
        }
        this.markers = [];
    }

    rechergerParkingByPays(pays: ReadPaysDTO) {
        this.parkingService.apiParkingsPaysPaysIdGet(pays.payS_Id).subscribe(
            {
                next : (result) => {                  
                    //this.parkings = result;
                    console.log(result);
                    this.afficherTousLesParkings(result);
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        );     
    }

    onDateDebutChange(event: any) {//pas utilisé
        //this.onParkingClick(this.parking);
    }

   

    annulerConnexion() {
        this.afficherDialogConnexion = false;
        sessionStorage.setItem('reservation', null);
    }

    seConnecter() {

        this.utilisateurForm.markAllAsTouched();
        if (this.utilisateurForm.valid) {
            //this.blockUI.start('Connexion en cours ...');
            this.authService.login(this.utilisateurForm.value.login, this.utilisateurForm.value.password,
                (placeCarUser) => {
                    this.afficherDialogConnexion = false;
                    this.blockUI.stop();
                    
                    console.log(placeCarUser['roles'].length);

                    if(placeCarUser['roles'].length > 1) {
                        placeCarUser['role'] = 'Client';
                        sessionStorage.setItem('placecar.user', JSON.stringify(placeCarUser));
                    }
                    else {
                        let role = placeCarUser['roles'][0];
                        placeCarUser['role'] = role;
                        sessionStorage.setItem('placecar.user', JSON.stringify(placeCarUser));
                    }

                    this.doReserver();

                }, (err) => {
                    console.log(err);
                    this.blockUI.stop();
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message });

                })
            return;
        }
        else {
            this.blockUI.stop();
            this.messageService.add({ severity: 'error', summary: 'Information', detail: 'Veuillez renseigner vos données de connexion' });
        } 
    }

    reserver() {
        
        if(this.selectedFormuleDePrix == null) {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: "Veillez sélectionner une formule de prix." });
            return;
        }

        if(this.selectedSpotFloor == null) {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: "Veillez sélectionner une place." });
            return;
        }

        var placeSelectionnee = this.placeStatusList[this.selectedSpotFloor];
        /*console.log(this.etageSelectione);
        console.log(this.placeStatusList);
        console.log(this.selectedSpotFloor);
        console.log(placeSelectionnee);*/

        let reservation = {
            "reS_DateDebut": this.dateDebutReservation,
            "reS_DateFin": this.dateFinReservation,
            "placeId": placeSelectionnee.placeId,
            "clientId": parseInt(this.authService.isLoggedIn() ? this.authService.getUser()['nameIdentifier'] : 0),
            "formPrixId": this.selectedFormuleDePrix.formuleId,
        }
        sessionStorage.setItem('reservation', JSON.stringify(reservation));

        if(!this.authService.isLoggedIn()) {
            this.afficherDialogConnexion = true;
            return;
        }

        this.doReserver();
    }

    doReserver() {

        console.log('doReserver');
        console.log(this.authService.isLoggedIn());
        console.log(this.authService.getUser()['roles'].includes('Client') > 0);

        if(this.authService.isLoggedIn() 
            && this.authService.getUser()['roles'].includes('Client') > 0) {
            let reservation = JSON.parse(sessionStorage.getItem('reservation'));
            reservation['clientId'] = this.authService.getUser()['nameIdentifier'] ;

            console.log(reservation);
            this.blockUI.start('Réservation en cours ...');
            this.reservationService.apiReservationsClientNewReservationPost(reservation).subscribe(
                {
                    next : (result) => {
                       
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Réservation effectuée avec succès'});
                        sessionStorage.setItem('reservation', null);

                        setTimeout(() => {
                            //this.router.navigate(['/gestion-reservations/lister-reservation']);

                             // Ouverture du menu de gauche
                            this.layoutService.state.staticMenuDesktopInactive = true;
                            this.layoutService.onMenuToggle();

                            //this.router.navigate(['/']);
                            this.router.navigateByUrl('/dummy', { skipLocationChange: true }).then(() => {
                                //this.router.navigate([''])
                                this.router.navigate(['gestion-reservations', 'lister-reservation']);
                            });
                           

                        }, 1000);
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
            this.messageService.add({ severity: 'error', summary: 'Information', detail: "Vous devez vous connecter en temps que client pour effectuer une réservation."});
        }
    }

    validerDisponibilites() {

        if(this.parking?.parK_Id > 0) {
              this.utilisateurForm.patchValue({
                'login' : ' ',
                'password': ''
              });

            //this.listerPlace(); 
            this.listerFormuleDePrix();
        }
        else {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: "Veillez sélectionner un parking." });
        }
    }

    listerFormuleDePrix() {
        this.dateForm.markAllAsTouched();
        if(this.dateForm.valid && this.parking?.parK_Id > 0)  {

            if(this.dateForm.value.dateFin != null && this.dateForm.value.dateFin !== '' && this.dateForm.value.dateFin < this.dateForm.value.dateDebut) {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: "Date de fin de la réservation ne peut pas être inférieure à la date de début." });
                return;
            }

            this.dateDebutReservation = this.dateForm.value.dateDebut;
            this.dateDebutReservation.setHours(this.dateForm.value.heureDebut?.replace(":00", ""), 0, 0, 0);
            this.dateDebutReservation = (this.dateDebutReservation as any).toISO();

            this.dateFinReservation = null;
            if(this.dateForm.value.dateFin !== null && this.dateForm.value.dateFin !== '') {
                this.dateFinReservation = this.dateForm.value.dateFin;

                if(this.dateForm.value.heureFin !== null && this.dateForm.value.heureFin !== '') {
                    this.dateFinReservation.setHours(this.dateForm.value.heureFin.replace(":00", ""), 0, 0, 0);
                    this.dateFinReservation = (this.dateFinReservation as any).toISO();
                }
                else {
                    this.dateFinReservation.setHours(0, 0, 0, 0);
                    this.dateFinReservation = (this.dateFinReservation as any).toISO();
                }
            }
            else if (this.dateForm.value.heureFin !== '' && this.dateForm.value.heureFin !== null) {
                this.dateFinReservation = new Date(this.dateForm.value.dateDebut);
                this.dateFinReservation.setHours(this.dateForm.value.heureFin.replace(":00", ""), 0, 0, 0);
                this.dateFinReservation = (this.dateFinReservation as any).toISO();

                if(parseInt(this.dateForm.value.heureFin.replace(":00", "")) < parseInt(this.dateForm.value.heureDebut.replace(":00", ""))) {
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: "L'heure de fin ne peut pas être inférieure à l'heure de début" });
                    return;
                }
            }

            this.formulePrixService.apiFormuleDePrixFormuleCorrespendanteParkingParkingIdGet(
                this.parking.parK_Id,
                this.dateDebutReservation as any,
                this.dateFinReservation as any
            ).subscribe(
                {
                    next : (result: FormuleOptionDTO[]) => {                  
                       
                        this.formuleOptionList = [];
                        this.selectedFormuleDePrix = null;
                        console.log(result);

                        if(result.length == 0) {

                                    //Récupération des formules existantes ur le parking
                                    this.formulePrixService.apiFormuleDePrixParkingParkingIdGet(this.parking.parK_Id ).subscribe(
                                        {
                                            next : (result: ReadForulesParkingDTO[]) => {
                                                this.LibelleFormules = [];
                                                for (let index = 0; index < result.length; index++) {
                                                    const element = result[index];
                                                    
                                                    element['data'] = element.forM_Title + ' - ' + element.forM_Prix + ' €';
                                                    this.LibelleFormules.push(element);
                                                }
                                                //this.formulePrixList = result;
                                            },
                                            error : (err) => {
                                                console.log(err);
                                                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                                            }
                                        }
                                    );


                            //this.messageService.add({ severity: 'info', summary: 'Information', detail: "Aucune formule de prix n'est disponible pour les dates choisies."});
                            this.afficherConfirmationReservationDialog = true;
                            return;
                        }

                        for(var i = 0; i < result.length; i++) {
                            var r = result[i];
                            r['index']= i;

                            this.formuleOptionList.push(r);
                        }
                        //this.formuleOptionList = result;
                       // this.placeLibreList = result;
                        //this.oPanel.toggle(null, document.querySelector('.address-col'));
                        this.afficherConfirmationReservationDialog = true;
                    },
                    error : (err) => {
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            ); 
        }
    }

    
    listerPlace(etage: number) {
        //this.oPanelTarget = target;
        //this.oPanelTarget = document.querySelector('.address-col')[0];
       // this.dateForm.markAllAsTouched();
       // if(this.dateForm.valid && this.parking?.parK_Id > 0)  {

            
            
            /*this.listerFormulePrix(this.parking);

            this.placeService.apiPlacesParkingLidreParkingParkingidDateDateGet(this.parking.parK_Id,
                (this.dateForm.value.dateDebut as Date).toISOString()
            ).subscribe(
                {
                    next : (result: ReadPlaceLibDTO[]) => {                  
                        this.placeLibreList = [];

                        for (let index = 0; index < result.length; index++) {
                            const element = result[index];
                            
                            element['data'] = 'Etage - ' + element.plA_Etage + ', P - ' + element.plA_NumeroPlace;
                            this.placeLibreList.push(element);
                        }
                       // this.placeLibreList = result;
                        //this.oPanel.toggle(null, document.querySelector('.address-col'));
                        this.afficherConfirmationReservationDialog = true;
                    },
                    error : (err) => {
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            );    */ 
        //}

        /*
        this.placeService.apiPlacesParkingLidreParkingParkingidDateDateGet(this.parking.parK_Id,
            (this.dateDebutReservation as any).toISO()
        ).subscribe(
            {
                next : (result: ReadPlaceLibDTO[]) => {       
                    console.log(result);           
                    this.placeLibreList = [];
                    this.parkingsFloor = [];

                    for (let index = 0; index < result.length; index++) {
                        const element = result[index];
                        
                        element['data'] = 'Etage - ' + element.plA_Etage + ', P - ' + element.plA_NumeroPlace;
                        this.placeLibreList.push(element);

                        if(!this.etageItems.includes(element.plA_Etage)) {
                            this.etageItems.push(element.plA_Etage);
                        }

                        this.parkingsFloor.push(new Parking(element.plA_Etage, element.plA_NumeroPlace, 0));
                    }

                    this.activeIndex = 1;
                },
                error : (err) => {
                    console.log(err);
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            }
        ); */

        this.placeService.apiPlacesParkingParkingParkingidEtageEtageIdGet(etage, this.parking.parK_Id,

            (new Date() as any).toISO(), this.dateDebutReservation as any,  this.dateFinReservation as any).subscribe(
                {
                    next : (result: PlaceStatusDto[]) => {       
                        console.log(result);    
                        this.placeStatusList = result;
                        //this.activeIndex = 1;
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

        /*this.placeService.apiPlacesParkingParkingParkingidEtageEtageIdGet(etage, this.parking.parK_Id,
            this.dateDebutReservation as any,).subscribe(
                {
                    next : (result: PlaceStatusDto[]) => {       
                        console.log(result);    
                        this.placeStatusList = result;
                        //this.activeIndex = 1;
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
            ); */
    }

    suivant() {

        if(this.activeIndex == 0) {

            if(this.etageItems.length == 0) {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: "Ce parking ne dispose pas de d'étage"});
                return;
            }

            if(this.selectedFormuleDePrix == null) {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: "Veillez sélectionner une formule de prix." });
                return;
            }

            this.etageSelectione = null;
            this.selectedSpotFloor = null;
            this.placeLibreForm.patchValue({
                'etage': ''
            });
            this.activeIndex = 1;
            //this.listerPlace(1);
        }
    }

    onSelectionEtageChange(event) {
        console.log(event);

        this.etageSelectione = event.value;
        this.selectedSpotFloor = null;
        this.listerPlace(this.etageSelectione);
    }

    onFormuleDePrixSelected(formuleDePrix) {
        this.selectedFormuleDePrix = formuleDePrix;
    }

    getStatus(index: number)
    {
        return this.status[index].status;
    }

    selectSpot(index: number, status: number) {
        console.log(status);
        if(status === 1) {
            console.log(this.selectedSpotFloor)
            if (this.selectedSpotFloor === index) {
                this.selectedSpotFloor = null;
            } else {

                this.selectedSpotFloor = index;
                //this.selectedSpotFloor = null;
            }
        }
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
