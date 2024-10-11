import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { identifierName } from '@angular/compiler';
import { TrensactionService } from 'src/app/main/api/generated';
@Component({
    templateUrl: './failure-checkout.component.html',
    providers: [MessageService]
})
export class FailureCheckoutComponent implements OnInit
{


    constructor(private activatedRoute:ActivatedRoute,
        private transactionService: TrensactionService,
        private router: Router
    ) {

    }
    ngOnInit(): void {
        const sessionId = this.activatedRoute.snapshot.queryParamMap.get('sessionId');

        const data = JSON.parse(sessionStorage.getItem(sessionId));
        console.log(data);
        if(data) {
            
            this.transactionService.apiTransactionsUpdateTransactionFailurePut(data.factureId,"Echec de Paiement, veuillez réessayer").subscribe(
                {
                    next: (result) =>{

                    },
                    error : (err) => {
                        console.log(err);
                        
                        //this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            )
        }
        else {
            //gestion-factures/lister-facture
            //this.router.navigate(['/gestion-factures/lister-facture']);
        }

        //throw new Error('Method not implemented.');


        /*Du coté failure
        1) récupérer la session id
        2) récupérer les données de la facture en session
        3) appeller le service transaction pour changer le statut de la transaction liée

        MAIS
        si pas de données de facture dans la session ou pas de ssessionid ==> tentative de hack ==> redirection vers le compoosant facture*/
    }

    retourAuxFactures() {
        this.router.navigateByUrl('/gestion-factures/lister-facture');
    }
     /*this.transactionService.apiTransactionsClient(this.authService.getUser()['nameIdentifier']).subscribe(
                {
                    next : (result) => {
                        console.log(result);

                        this.transactions = [];

                        for (let index = 0; index < result.length; index++) {
                            const element = result[index];
                            
                            element['tranS_Date'] = element['tranS_Date']?.substring(0, 19).replace('T', ' ');
                            this.transactions.push(element);
                        }
                        //this.transactions = result;
                        this.loading = false;
                    },
                   
                    error : (err) => {
                        console.log(err);
                        this.loading = false;
                        //this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});
                    }
                }
            );*/
}
