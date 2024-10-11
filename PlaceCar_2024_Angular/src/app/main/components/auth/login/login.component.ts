import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { AddResDTO, ReservationService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
        .selection-card:hover {
            border: 1px solid var(--primary-color);
        }
    `],
    providers: [ConfirmationService, MessageService]
})
export class LoginComponent implements OnInit {

    utilisateurForm: FormGroup;
    roleItems: any[] = [];
    selectedRole: any;
    afficherDialogSelectionRole = false;
    placeCarUser: any;

    @BlockUI() blockUI: NgBlockUI;

    constructor(public layoutService: LayoutService,
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private router: Router,
        private confirmationService: ConfirmationService,
        private messageService: MessageService,
        private reservationService: ReservationService) { }

    ngOnInit(){

        this.utilisateurForm = this.formBuilder.group({
            login: [sessionStorage.getItem('placecar.username'), [Validators.required]],
            password: [sessionStorage.getItem('placecar.password'), [Validators.required]],
            rememberMe: ['']
          });
    }

    doLogin() {

        this.blockUI.start('Connexion en cours ...');

        this.utilisateurForm.markAllAsTouched();

        if(this.utilisateurForm.valid) {
            this.authService.login(this.utilisateurForm.value.login, this.utilisateurForm.value.password,
                (placeCarUser) => {

                    this.blockUI.stop();
                    //if (this.authService.isLoggedIn()) {

                        if(this.utilisateurForm.value.rememberMe) {
                            sessionStorage.setItem('placecar.username', this.utilisateurForm.value.login);
                            sessionStorage.setItem('placecar.password', this.utilisateurForm.value.password);
                        }

                        this.placeCarUser = placeCarUser;
                        console.log(this.placeCarUser['roles'].length);

                        if(this.placeCarUser['roles'].length > 1) {
                            this.roleItems = this.placeCarUser['roles'];

                            this.afficherDialogSelectionRole = true;
                        }
                        else {
                            let role = this.placeCarUser['roles'][0];
                            this.placeCarUser['role'] = role;
                            sessionStorage.setItem('placecar.user', JSON.stringify(this.placeCarUser));

                             // Ouverture du menu de gauche
                            this.layoutService.state.staticMenuDesktopInactive = true;
                            this.layoutService.onMenuToggle();

                            this.gotoHomePage();
                        }
                   // }

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

    validerSelectionRole() {

        console.log(this.selectedRole);

        if(this.selectedRole === undefined || this.selectedRole === null) {
            this.messageService.add({ severity: 'error', summary: 'Information', detail: 'Aucun rôle sélectionné' });
            return;
        }

        this.placeCarUser['role'] = this.selectedRole;
        sessionStorage.setItem('placecar.user', JSON.stringify(this.placeCarUser));

        this.selectedRole = null;

         // Ouverture du menu de gauche
         this.layoutService.state.staticMenuDesktopInactive = true;
         this.layoutService.onMenuToggle();

         this.gotoHomePage();
    }

    onRoleSelected(role: String) {
        this.selectedRole = role;
        this.validerSelectionRole();
    }

    gotoHomePage() {
       let reservation = sessionStorage.getItem('reservation');
       console.log(reservation);

       if(reservation && reservation?.length > 0
        && reservation != 'null' && this.authService.isLoggedIn()) {
            reservation = JSON.parse(reservation);
            reservation['clientId'] = this.authService.getUser()['nameIdentifier'];

            this.confirmationService.confirm({
                key: 'confirmDialog',
                //target: event.target || new EventTarget,
                header: 'Confirmation',
                message: 'Il y a une réservation en cours, voulez-vous la confirmer ? <br> ' +
                'Date début: ' + reservation['reS_DateDebut'] + ', Date fin: ' + reservation['reS_DateFin'],
                icon: 'pi pi-exclamation-triangle',
                acceptLabel: 'Oui',
                rejectLabel: 'Non',
                accept: () => {

                    this.blockUI.start('Réservation en cours ...');
                    this.reservationService.apiReservationsClientNewReservationPost(reservation as AddResDTO).subscribe(
                        {
                            next : (result) => {

                                this.blockUI.stop();
                                this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Réservation effectuée avec succès'});

                                setTimeout(() => {
                                    //this.router.navigate(['/gestion-reservations/lister-reservation']);

                                     // Ouverture du menu de gauche
                                    this.layoutService.state.staticMenuDesktopInactive = true;
                                    this.layoutService.onMenuToggle();

                                    sessionStorage.setItem('reservation', null);

                                    this.router.navigate(['gestion-reservations','lister-reservation']);
                                }, 2000);
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
                    sessionStorage.setItem('reservation', null);

                    this.router.navigate(['']);
                }
            });
       }
       else {
        this.router.navigate(['']);
       }
    }
}
