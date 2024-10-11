import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { identifierName } from '@angular/compiler';
@Component({
    templateUrl: './success-checkout.component.html',
    providers: [MessageService]
})
export class SuccessCheckoutComponent implements OnInit
{


    constructor(private activatedRoute:ActivatedRoute,
        private router: Router
    ) {

    }
    ngOnInit(): void {
        const SessionId = this.activatedRoute.snapshot.queryParamMap.get('sessionId');

        const data = JSON.parse(sessionStorage.getItem(SessionId));
        throw new Error('Method not implemented.');


        
    }

    retourAuxFactures() {
        this.router.navigateByUrl('/gestion-factures/lister-facture');
    }

    /*Du coté failure
        1) récupérer la session id
        2) récupérer les données de la facture en session
        3) appeller le service transaction pour changer le statut de la transaction liée

        MAIS
        si pas de données de facture dans la session ou pas de ssessionid ==> tentative de hack ==> redirection vers le compoosant facture*/
}
