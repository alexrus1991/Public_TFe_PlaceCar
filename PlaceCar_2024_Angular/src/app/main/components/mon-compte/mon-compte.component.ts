import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ClientColtrollerService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './mon-compte.component.html',
    providers: [MessageService]
})
export class MonCompteComponent implements OnInit {

    utilisateurForm: FormGroup;
    modificationMotDePasseForm: FormGroup;
    afficherDialogModificationMotDePasse = false;
     
    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private clientColtrollerService: ClientColtrollerService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute,
        private authService: AuthService,
        private router: Router
    ) {
    }

    ngOnInit() {

        this.utilisateurForm = this.formBuilder.group({
            nom: [{value : '', disabled : true}],
            prenom: [{value : '', disabled : true}],
            dateNaissance: [{value : '', disabled : true}],
            email: [{value : '', disabled : true}],
            //password: ['', [Validators.required]],
          });
        this.modificationMotDePasseForm = this.formBuilder.group({
            newPassword: ['', Validators.required],
            newPassword2: ['', Validators.required],
          });
          

          //this.blockUI.start('Chargement en cours ...');

          this.clientColtrollerService.apiClientsIdGet(this.authService.getUser()['nameIdentifier']).subscribe({
            next : (result) => {
                this.blockUI.stop();
                console.log(result);

                this.utilisateurForm.patchValue({
                    'nom': result?.perS_Nom,
                    'prenom': result?.perS_Prenom,
                    'dateNaissance': new Date(result?.perS_DateNaissance),
                    'email' : result?.perS_Email
                })
            },   
            error : (err) => {
                console.log(err);
                this.blockUI.stop();
                this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
            }
        });    
    }

    modifier() {
      
        this.utilisateurForm.markAllAsTouched();
        if(this.utilisateurForm.valid) {

            console.log(this.utilisateurForm.value);
            this.blockUI.start('Modification en cours ...');
            
          this.clientColtrollerService.apiClientsClientClientIdPut(this.authService.getUser()['nameIdentifier'], {
            id : this.authService.getUser()['nameIdentifier'],
            pwd: this.utilisateurForm.value.password
          }).subscribe({
                next : () => {
                    this.blockUI.stop();
                    this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Modification effectuée avec succès'});

                },   
                error : (err) => {
                    console.log(err);
                    this.blockUI.stop();
                    this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                }
            });    
        }
    }

    modifierPassword() {
        this.afficherDialogModificationMotDePasse = true;
    }

    annulerModificationMotDePasse() {
        this.afficherDialogModificationMotDePasse = false;
    }

    validerModificationMotDePasse() {

        this.modificationMotDePasseForm.markAllAsTouched();
        if(this.modificationMotDePasseForm.valid) {

            if(this.modificationMotDePasseForm.value.newPassword === this.modificationMotDePasseForm.value.newPassword2) {

                this.blockUI.start('Modification en cours ...');
            
                this.clientColtrollerService.apiClientsClientClientIdPut(this.authService.getUser()['nameIdentifier'], {
                  id : this.authService.getUser()['nameIdentifier'],
                  pwd: this.modificationMotDePasseForm.value.newPassword 
                }).subscribe({
                      next : () => {
                          this.blockUI.stop();
                          this.messageService.add({ severity: 'success', summary: 'Information', detail: 'Modification effectuée avec succès'});
      
                      },   
                      error : (err) => {
                          console.log(err);
                          this.blockUI.stop();
                          this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                      }
                  });   
            }
            else {
                this.messageService.add({ severity: 'error', summary: 'Information', detail: 'Les mots de passe ne sont pas identique'});
            }  
        }
    }
}
