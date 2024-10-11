import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewInit, HostListener } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { Product } from '../../../api/product';
import { ProductService } from '../../../service/product.service';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { Router } from '@angular/router';
import { CountryService } from '../../../service/country.service';
import { GeoapifyService, PaysService, ReadPaysDTO } from '../../../api/generated';
import { AuthService } from '../../../service/auth.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

import * as L from 'leaflet';

@Component({
    templateUrl: './creer-reservation.component.html',
    styleUrls: ['./creer-reservation.component.css'],
    providers: [MessageService]
})
export class CreerReservationComponent implements OnInit, OnDestroy, AfterViewInit {

    items!: MenuItem[];

    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;
    
    countries: any[] = [];
    filteredCountries: any[] = [];
    country: any;

    @BlockUI() blockUI: NgBlockUI;

    private markers: any[] = [];
    @ViewChild('mapContainer') private mapContainer: ElementRef;
    private map: any;

    defaultIcon = L.icon({
        iconUrl: 'assets/demo/images/leaflet/marker-icon-new.png',
        shadowUrl: 'assets/demo/images/leaflet/marker-shadow.png',
        //iconSize: [24,36],
        iconSize: [32,32],
        iconAnchor: [12,36]
      });

    constructor(public layoutService: LayoutService, 
        public router: Router,
        private countryService: CountryService,
        private paysService: PaysService,
        public authService: AuthService,
        private geoapifyService: GeoapifyService,
        private messageService: MessageService,
    ) { 

       // this.menuButton.nativeElement.style.display='none';
        L.Marker.prototype.options.icon = this.defaultIcon;
    }

    ngOnInit() {
        //document.getElementById('creer-reservation').style.width = /*(window.innerWidth - 600 ) +*/ '100em';
        /*this.countryService.getCountries().then(countries => {
            this.countries = countries;
        });*/
        /*
        if(!this.authService.isLoggedIn()) {
            this.layoutService.state.staticMenuDesktopInactive = false;
            this.layoutService.onMenuToggle();
        } 
        else {
            this.layoutService.state.staticMenuDesktopInactive = true;
            this.layoutService.onMenuToggle();
        }*/
    }

    ngOnDestroy() {
        //this.map.remove();
    }

    ngAfterViewInit(): void {
//carte Leaflet est initialisée
        console.log('ngAfterViewInit');
        this.map = L.map(this.mapContainer.nativeElement, {
            center: [ 39.8282, -98.5795 ],
            zoom: 5,
            //zoomControl: false
         });
         const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 18,
            minZoom: 5,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
          });
      
        tiles.addTo(this.map);
        //this.map.setView([ 39.8282, -98.5795 ], this.map.getZoom(), {animate: true});

        this.paysService.apiPaysGet().subscribe({
            next : (result: ReadPaysDTO[]) => {
                //this.blockUI.stop();
                this.countries = result;
                this.afficherTousLesPays();

                
            },
            error : (err) => {
                //this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});

                //this.errorMessage = err.error ? err.error.message ?? err.message : err.message;
            }
        });  

      

    }

    afficherTousLesPays() {

        //this.blockUI.start('Recherche en cours ...');

        this.removeAllMarkers();
        for (let index = 0; index < this.countries.length; index++) {
            const element = this.countries[index];
            
            let pays = element.payS_Nom;
            this.geocodeAddress(pays, (result: any) => {
                console.log(pays);
                console.log(result);

                this.addMarker(result.properties.lat, result.properties.lon, {
                    icon: new L.DivIcon({
                        className: 'my-div-icon',
                        html: '<div class="my-div-span"><h5 style="color: blue">' + result.properties.country + '</h5>' +
                        '<img src="assets/demo/images/flag/flag_placeholder.png" class="flag flag-' + result.properties.country_code + '" width="30"></div>'
                    }),
                    country: element
                });

                if(index == this.countries.length - 1) {
                    this.blockUI.stop();
                }
            });
        }
    }

    addMarker(lat: number, lon: number, options: any = {}) {
        let marker = L.marker(new L.LatLng(lat, lon), options /*{
            icon: L.icon({
                iconUrl: address?.toLowerCase().indexOf(',') <= 0 ? 'assets/demo/images/leaflet/marker-icon-country.png' : 'assets/demo/images/leaflet/marker-icon.png',
                shadowUrl: 'assets/demo/images/leaflet/marker-shadow.png',
                iconSize: [48,48],
                iconAnchor: [12,36]
            })
        }*/);
        this.markers.push(marker);

        //marker.addTo(this.map);
        var group = L.featureGroup(this.markers).addTo(this.map).on('click', (event) => {
            this.router.navigateByUrl('/gestion-reservations/creer-reservation-map', { state: { pays : event.sourceTarget.options.country } });
        });
        this.map.fitBounds(group.getBounds(), {
            maxZoom: 10
        });
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

    searchCountry(event: any) {
        // in a real application, make a request to a remote url with the query and
        // return filtered results, for demo we filter at client side
        const filtered: any[] = [];
        const query = event.query;
        // tslint:disable-next-line:prefer-for-of
        for (let i = 0; i < this.countries.length; i++) {
            const country = this.countries[i];
            if (country.payS_Nom?.toLowerCase().indexOf(query.toLowerCase()) == 0) {
                filtered.push(country);
            }
        }

        this.filteredCountries = filtered;
    }

    onPaysSelect(event: any) {
        //this.map.off();
        //this.blockUI.start('Redirection en cours ...');
        this.router.navigateByUrl('/gestion-reservations/creer-reservation-map', { state: { pays : event.value } });
        //this.blockUI.stop();
    }

    geocodeAddress(address: any, callback: any) {

        this.geoapifyService.addressGet(address).subscribe({
            next: (result) => {
                
                let featureCollection = JSON.parse(result);

                if (featureCollection.features.length === 0) {
                    return;
                }

                const foundAddress = featureCollection.features[0];
                callback(foundAddress);

            },
            error: (err) => {

            }
        });
    }
}
