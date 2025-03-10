import { AfterViewInit, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from '../service/app.layout.service';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit, AfterViewInit {
//menu dynamique, généré en fonction du rôle de l'utilisateur connecté.
    model: any[] = [];

    constructor(public layoutService: LayoutService, private authService: AuthService) { }

    ngOnInit() {
//Initialise le modèle de menu. Si l'utilisateur n'est pas connecté, affiche un menu par défaut avec uniquement une option de page d'accueil
        this.model = [];

        if(!this.isLoggedIn()) {
            this.model.push({
                label: 'Home',
                items: [
                    { label: 'Accueil', icon: 'pi pi-fw', routerLink: ['/'] },
                    //{ label: 'Réservation', icon: 'pi pi-fw', routerLink: ['/reservation'] },
                ]
            });
        }


        /*this.model.push({
                label: 'UI Components',
                items: [
                    { label: 'Form Layout', icon: 'pi pi-fw pi-id-card', routerLink: ['/uikit/formlayout'] },
                    { label: 'Input', icon: 'pi pi-fw pi-check-square', routerLink: ['/uikit/input'] },
                    { label: 'Float Label', icon: 'pi pi-fw pi-bookmark', routerLink: ['/uikit/floatlabel'] },
                    { label: 'Invalid State', icon: 'pi pi-fw pi-exclamation-circle', routerLink: ['/uikit/invalidstate'] },
                    { label: 'Button', icon: 'pi pi-fw pi-box', routerLink: ['/uikit/button'] },
                    { label: 'Table', icon: 'pi pi-fw pi-table', routerLink: ['/uikit/table'] },
                    { label: 'List', icon: 'pi pi-fw pi-list', routerLink: ['/uikit/list'] },
                    { label: 'Tree', icon: 'pi pi-fw pi-share-alt', routerLink: ['/uikit/tree'] },
                    { label: 'Panel', icon: 'pi pi-fw pi-tablet', routerLink: ['/uikit/panel'] },
                    { label: 'Overlay', icon: 'pi pi-fw pi-clone', routerLink: ['/uikit/overlay'] },
                    { label: 'Media', icon: 'pi pi-fw pi-image', routerLink: ['/uikit/media'] },
                    { label: 'Menu', icon: 'pi pi-fw pi-bars', routerLink: ['/uikit/menu'], routerLinkActiveOptions: { paths: 'subset', queryParams: 'ignored', matrixParams: 'ignored', fragment: 'ignored' } },
                    { label: 'Message', icon: 'pi pi-fw pi-comment', routerLink: ['/uikit/message'] },
                    { label: 'File', icon: 'pi pi-fw pi-file', routerLink: ['/uikit/file'] },
                    { label: 'Chart', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/uikit/charts'] },
                    { label: 'Misc', icon: 'pi pi-fw pi-circle', routerLink: ['/uikit/misc'] }
                ]
            },
            {
                label: 'Prime Blocks',
                items: [
                    { label: 'Free Blocks', icon: 'pi pi-fw pi-eye', routerLink: ['/blocks'], badge: 'NEW' },
                    { label: 'All Blocks', icon: 'pi pi-fw pi-globe', url: ['https://www.primefaces.org/primeblocks-ng'], target: '_blank' },
                ]
            },
            {
                label: 'Utilities',
                items: [
                    { label: 'PrimeIcons', icon: 'pi pi-fw pi-prime', routerLink: ['/utilities/icons'] },
                    { label: 'PrimeFlex', icon: 'pi pi-fw pi-desktop', url: ['https://www.primefaces.org/primeflex/'], target: '_blank' },
                ]
            },
            {
                label: 'Pages',
                icon: 'pi pi-fw pi-briefcase',
                items: [
                    {
                        label: 'Landing',
                        icon: 'pi pi-fw pi-globe',
                        routerLink: ['/landing']
                    },
                    {
                        label: 'Auth',
                        icon: 'pi pi-fw pi-user',
                        items: [
                            {
                                label: 'Login',
                                icon: 'pi pi-fw pi-sign-in',
                                routerLink: ['/auth/login']
                            },
                            {
                                label: 'Error',
                                icon: 'pi pi-fw pi-times-circle',
                                routerLink: ['/auth/error']
                            },
                            {
                                label: 'Access Denied',
                                icon: 'pi pi-fw pi-lock',
                                routerLink: ['/auth/access']
                            }
                        ]
                    },
                    {
                        label: 'Crud',
                        icon: 'pi pi-fw pi-pencil',
                        routerLink: ['/pages/crud']
                    },
                    {
                        label: 'Timeline',
                        icon: 'pi pi-fw pi-calendar',
                        routerLink: ['/pages/timeline']
                    },
                    {
                        label: 'Not Found',
                        icon: 'pi pi-fw pi-exclamation-circle',
                        routerLink: ['/notfound']
                    },
                    {
                        label: 'Empty',
                        icon: 'pi pi-fw pi-circle-off',
                        routerLink: ['/pages/empty']
                    },
                ]
            },
            {
                label: 'Hierarchy',
                items: [
                    {
                        label: 'Submenu 1', icon: 'pi pi-fw pi-bookmark',
                        items: [
                            {
                                label: 'Submenu 1.1', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    { label: 'Submenu 1.1.1', icon: 'pi pi-fw pi-bookmark' },
                                    { label: 'Submenu 1.1.2', icon: 'pi pi-fw pi-bookmark' },
                                    { label: 'Submenu 1.1.3', icon: 'pi pi-fw pi-bookmark' },
                                ]
                            },
                            {
                                label: 'Submenu 1.2', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    { label: 'Submenu 1.2.1', icon: 'pi pi-fw pi-bookmark' }
                                ]
                            },
                        ]
                    },
                    {
                        label: 'Submenu 2', icon: 'pi pi-fw pi-bookmark',
                        items: [
                            {
                                label: 'Submenu 2.1', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    { label: 'Submenu 2.1.1', icon: 'pi pi-fw pi-bookmark' },
                                    { label: 'Submenu 2.1.2', icon: 'pi pi-fw pi-bookmark' },
                                ]
                            },
                            {
                                label: 'Submenu 2.2', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    { label: 'Submenu 2.2.1', icon: 'pi pi-fw pi-bookmark' },
                                ]
                            },
                        ]
                    }
                ]
            },
            {
                label: 'Get Started',
                items: [
                    {
                        label: 'Documentation', icon: 'pi pi-fw pi-question', routerLink: ['/documentation']
                    },
                    {
                        label: 'View Source', icon: 'pi pi-fw pi-search', url: ['https://github.com/primefaces/sakai-ng'], target: '_blank'
                    }
                ]
            }
        );*/
    }

    ngAfterViewInit() {
   //vérifie le rôle de l'utilisateur via authService et personnalise le menu    
        if(this.isLoggedIn()) {

            if(this.authService.getUser()['role'].toLowerCase() === 'client') {
                this.model.push({
                    label: 'Home',
                    items: [
                        { label: 'Accueil', icon: 'pi pi-fw', routerLink: ['/'] },
                        //{ label: 'Informations', icon: 'pi pi-fw', routerLink: ['/'] },
                        { label: 'Factures', icon: 'pi pi-fw', routerLink: ['/gestion-factures/lister-facture'] },
                        { label: 'Préférences', icon: 'pi pi-fw', routerLink: ['/gestion-preferences/lister-preference'] },
                        { label: 'Mes réservations', icon: 'pi pi-fw', routerLink: ['/gestion-reservations/lister-reservation'] },
                        { label: 'Transactions', icon: 'pi pi-fw', routerLink: ['/transactions'] },
                    ]
                });
            }

            if(this.authService.getUser()['role'].toLowerCase() === 'employee') {
                this.model.push({
                    label: 'Home',
                    items: [
                        { label: 'Accueil', icon: 'pi pi-fw', routerLink: ['/'] },
                        { label: 'Employés', icon: 'pi pi-fw', routerLink: ['/employe/gestion-employe/lister-employe'] },
                        { label: 'Formule prix', icon: 'pi pi-fw', routerLink: ['/employe/gestion-formule-prix/lister-formule-prix'] },
                        { label: 'Vue parking', icon: 'pi pi-fw', routerLink: ['/employe/gestion-parking/detail-parking'] },
                        //{ label: 'Informations', icon: 'pi pi-fw', routerLink: ['/'] },
                        //{ label: 'Préférences', icon: 'pi pi-fw', routerLink: ['/'] },
                    ]
                });
            }

            if(this.authService.getUser()['role'].toLowerCase() === 'admin' || this.authService.getUser()['role'].toLowerCase() === 'superadmin') {
                
                let items = [
                    { label: 'Accueil', icon: 'pi pi-fw', routerLink: ['/'] },
                    //{ label: 'Informations', icon: 'pi pi-fw', routerLink: ['/'] },
                    //{ label: 'Transactions', icon: 'pi pi-fw', routerLink: ['/'] },
                    { label: 'Parking', icon: 'pi pi-fw', routerLink: ['/admin/gestion-parking/lister-parking'] },
                    //{ label: 'Gestion employés', icon: 'pi pi-fw', routerLink: ['/admin/gestion-employe/lister-employe'] },
                    { label: 'Gestion pays', icon: 'pi pi-fw', routerLink: ['/admin/gestion-pays/lister-pays'] },
                    { label: 'Gestion formule type', icon: 'pi pi-fw', routerLink: ['/admin/gestion-formule-type/lister-formule-type'] },
                    { label: 'Transactions', icon: 'pi pi-fw', routerLink: ['/transactions'] },
                ];
                if(this.authService.getUser()['role'].toLowerCase() === 'superadmin') {
                    items.push({
                        label: 'Créer admin', icon: 'pi pi-fw', routerLink: ['/admin/gestion-employe/ajouter-admin']
                    })
                }
                
                this.model.push({
                    label: 'Home',
                    items: items
                },
                {
                    label: 'Gestion employé',
                    items: [
                        { label: 'Lister employé', icon: 'pi pi-fw', routerLink: ['/admin/gestion-employe/lister-employe'] },
                        { label: 'Ajouter employé', icon: 'pi pi-fw', routerLink: ['/admin/gestion-employe/ajouter-employe'] },
                    ]
                });
            }
        }
    }

    isLoggedIn() {//Vérifie si l'utilisateur est connecté
        return this.authService.isLoggedIn();
    }
}
