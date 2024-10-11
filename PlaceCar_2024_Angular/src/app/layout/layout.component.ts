import { Component, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter, Subscription } from 'rxjs';
import { LayoutService } from "./service/app.layout.service";
import { AppSidebarComponent } from "./sidebar/app.sidebar.component";
import { AppTopBarComponent } from './topbar/app.topbar.component';
import { AuthService } from '../main/service/auth.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AddResDTO, ReservationService } from '../main/api/generated';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styles: [`
        .selection-card:hover {
            border: 1px solid var(--primary-color);
        }
    `],
    providers: [ConfirmationService, MessageService]
})
export class LayoutComponent implements OnInit, OnDestroy {

    overlayMenuOpenSubscription: Subscription;

    menuOutsideClickListener: any;

    profileMenuOutsideClickListener: any;

    @ViewChild(AppSidebarComponent) appSidebar!: AppSidebarComponent;

    @ViewChild(AppTopBarComponent) appTopbar!: AppTopBarComponent;
    roleItems: any[] = [];
    selectedRole: any;
    afficherDialogSelectionRole = false;

    constructor(public layoutService: LayoutService, public renderer: Renderer2, public router: Router, private authService: AuthService,
        private confirmationService: ConfirmationService,
        private reservationService: ReservationService,
        private messageService: MessageService
    ) {
        //écoute pour les clics en dehors du menu (fermeture automatique du menu)
        this.overlayMenuOpenSubscription = this.layoutService.overlayOpen$.subscribe(() => {//code s’abonne à l’événement overlayOpen$ provenant de layoutService. -->
            if (!this.menuOutsideClickListener) {                //Cet événement est probablement émis pour indiquer que le menu en superposition (overlay) doit s'ouvrir.
                this.menuOutsideClickListener = this.renderer.listen('document', 'click', event => {
                    const isOutsideClicked = !(this.appSidebar.el.nativeElement.isSameNode(event.target) || this.appSidebar.el.nativeElement.contains(event.target) 
                        || this.appTopbar.menuButton.nativeElement.isSameNode(event.target) || this.appTopbar.menuButton.nativeElement.contains(event.target));
                    //crée un écouteur qui se déclenche lorsque l'utilisateur clique
                    if (isOutsideClicked) {// vérifie si l'élément cliqué n'est ni le menu latéral (appSidebar) ni le bouton du menu
                        this.hideMenu();
                    }
                });
            }

            if (!this.profileMenuOutsideClickListener) {
                this.profileMenuOutsideClickListener = this.renderer.listen('document', 'click', event => {
                    const isOutsideClicked = !(this.appTopbar.menu.nativeElement.isSameNode(event.target) || this.appTopbar.menu.nativeElement.contains(event.target)
                        || this.appTopbar.topbarMenuButton.nativeElement.isSameNode(event.target) || this.appTopbar.topbarMenuButton.nativeElement.contains(event.target));

                    if (isOutsideClicked) {
                        this.hideProfileMenu();//Si le clic extérieur ,appele pour fermer le menu de profil.
                    }
                });
            }
//Si le menu mobile est actif (staticMenuMobileActive), blockBodyScroll() est appelé pour désactiver le défilement de la page.
            if (this.layoutService.state.staticMenuMobileActive) {
                this.blockBodyScroll();
            }
        });

        this.router.events.pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(() => {
                this.hideMenu();
                this.hideProfileMenu();
            });
    }

    hideMenu() {
        this.layoutService.state.overlayMenuActive = false;
        this.layoutService.state.staticMenuMobileActive = false;
        this.layoutService.state.menuHoverActive = false;
        if (this.menuOutsideClickListener) {
            this.menuOutsideClickListener();
            this.menuOutsideClickListener = null;
        }
        this.unblockBodyScroll();
    }

    hideProfileMenu() {
        this.layoutService.state.profileSidebarVisible = false;
        if (this.profileMenuOutsideClickListener) {
            this.profileMenuOutsideClickListener();
            this.profileMenuOutsideClickListener = null;
        }
    }

    blockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.add('blocked-scroll');
        }
        else {
            document.body.className += ' blocked-scroll';
        }
    }

    unblockBodyScroll(): void {
        if (document.body.classList) {
            document.body.classList.remove('blocked-scroll');
        }
        else {
            document.body.className = document.body.className.replace(new RegExp('(^|\\b)' +
                'blocked-scroll'.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
        }
    }

    get containerClass() {
        return {
            'layout-theme-light': this.layoutService.config().colorScheme === 'light',
            'layout-theme-dark': this.layoutService.config().colorScheme === 'dark',
            'layout-overlay': this.layoutService.config().menuMode === 'overlay',
            'layout-static': this.layoutService.config().menuMode === 'static',
            'layout-static-inactive': this.layoutService.state.staticMenuDesktopInactive && this.layoutService.config().menuMode === 'static',
            'layout-overlay-active': this.layoutService.state.overlayMenuActive,
            'layout-mobile-active': this.layoutService.state.staticMenuMobileActive,
            'p-input-filled': this.layoutService.config().inputStyle === 'filled',
            'p-ripple-disabled': !this.layoutService.config().ripple
        }
    }

    ngOnInit(): void {
        if(!this.authService.isLoggedIn()) {
            this.layoutService.state.staticMenuDesktopInactive = false;
            this.layoutService.onMenuToggle();
            this.router.navigateByUrl('/');
        } 
        else {
            this.layoutService.state.staticMenuDesktopInactive = true;
            this.layoutService.onMenuToggle();
        }
    }

    ngOnDestroy() {
        if (this.overlayMenuOpenSubscription) {
            this.overlayMenuOpenSubscription.unsubscribe();
        }

        if (this.menuOutsideClickListener) {
            this.menuOutsideClickListener();
        }
    }

    isLoggedIn() {
        return this.authService.isLoggedIn();
    }

    getUser() {
        return this.authService.getUser();
    }

    seConnecterEnTemsQue() {
        this.roleItems = this.getUser()['roles'];
        this.afficherDialogSelectionRole = true;
    }

    onRoleSelected(role: String) {

        if(role === this.getUser()['role']) {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: 'Vous êtes déjà connecté en tant que [' + role + ']' });
            return;
        }
        
        this.afficherDialogSelectionRole = false;
        this.selectedRole = role;
        this.validerSelectionRole();
    }

    validerSelectionRole() {

        console.log(this.selectedRole);

        if(this.selectedRole === undefined || this.selectedRole === null) {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: 'Aucun rôle sélectionné' });
            return;
        }

        var user = this.getUser();
        user['role'] = this.selectedRole;
        sessionStorage.setItem('placecar.user', JSON.stringify(user));//Sauvegarde l’utilisateur avec son nouveau rôle

        this.selectedRole = null;//null pour éviter les sélections persistantes

        
         this.layoutService.state.staticMenuDesktopInactive = true; // Ouverture du menu de gauche
         this.layoutService.onMenuToggle();//  fonction qui met à jour l’état du menu

         this.router.navigateByUrl('/dummy', { skipLocationChange: true }).then(() => {
            this.router.navigate(['']);//naviguer vers une URL temporaire '/dummy' sans changer l’URL visible dans la barre d’adresse
        });
    }
}
