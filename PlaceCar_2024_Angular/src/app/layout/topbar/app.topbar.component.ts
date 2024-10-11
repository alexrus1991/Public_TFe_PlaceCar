import { Component, ElementRef, ViewChild } from '@angular/core';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { LayoutService } from "../service/app.layout.service";
import { AuthService } from 'src/app/main/service/auth.service';
import { Router } from '@angular/router';
import { ClientColtrollerService } from 'src/app/main/api/generated';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html',
    providers: [ConfirmationService, MessageService]
})
export class AppTopBarComponent {

    items!: MenuItem[];

    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;
    @BlockUI() blockUI: NgBlockUI;

    constructor(public layoutService: LayoutService,
        private authService:
        AuthService,
        private router: Router,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
    private clientColtrollerService: ClientColtrollerService) { }

    reload() {

        this.router.navigateByUrl('/dummy', { skipLocationChange: true }).then(() => {
            this.router.navigate([''])});
    }

    logout() {
        this.authService.logout();

        // Fermeture du menu de gauche
        this.layoutService.state.staticMenuDesktopInactive = false;
        this.layoutService.onMenuToggle();
        this.reload();
    }

    isLoggedIn() {
        return this.authService.isLoggedIn();
    }

    getUser() {
        return this.authService.getUser();
    }

    creerCompteClient() {

        this.confirmationService.confirm({
            key: 'confirmDialog',
            //target: event.target || new EventTarget,
            header: 'Confirmation',
            message: 'Voulez-vous vraiment créer un compte client ?',
            icon: 'pi pi-exclamation-triangle',
            acceptLabel: 'Oui',
            rejectLabel: 'Non',
            accept: () => {

                this.blockUI.start('Opération en cours ...');
                this.clientColtrollerService.apiClientsEmployeeEmployeeIdPost(this.getUser()['nameIdentifier']).subscribe(
                    {
                        next : (result) => {
                            this.blockUI.stop();
                            this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Compte client crée avec succès'});

                            setTimeout(() => {

                                let user = this.getUser();
                                let roles = user['roles'];
                                roles.push('Client');

                                user['role'] = 'Client';
                                user['roles'] = roles;

                                sessionStorage.setItem('placecar.user', JSON.stringify(user));
                                this.reload();

                            }, 1000);
                        },
                        error : (err) => {
                            this.blockUI.stop();
                            console.log(err);
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                        }
                    }
                );

            },
            reject: () => {
            }
        });
    }

    seConnecterEnTemsQue(role: any) {

        let user = this.getUser();
        user['role'] = role; //Le jwt n'est plus correcte donc il faut forcer la reconnexion

        sessionStorage.setItem('placecar.user', JSON.stringify(user));
        this.reload();
    }

}
