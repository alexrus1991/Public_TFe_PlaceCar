import { Component, Injectable, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { UserService } from 'src/app/main/api/generated';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `],
    providers: [MessageService]
})
export class RegisterComponent implements OnInit {

    utilisateurForm: FormGroup;
    @BlockUI() blockUI: NgBlockUI;

    constructor(public layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private messageService: MessageService,
        private userService: UserService) { }

    ngOnInit() {

        this.utilisateurForm = this.formBuilder.group({
            perS_Nom: ['', [Validators.required]],
            perS_Prenom: ['', [Validators.required]],
            perS_DateNaissance: ['', [Validators.required]],
            perS_Email: ['', [Validators.required]],
            perS_Password: ['', [Validators.required]],
          });
    }

    creer() {
      
        this.utilisateurForm.markAllAsTouched();
        if(this.utilisateurForm.valid) {

            this.blockUI.start('Opération en cours ...');
            this.userService.apiUsersNouveauClientPost(this.utilisateurForm.value).subscribe(
                {
                    next : (result) => {
                        this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Utilisateur crée avec succès' });
                    },
                    error : (err) => {
                        this.blockUI.stop();
                        console.log(err);
                        this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            )
        }
    }

}
