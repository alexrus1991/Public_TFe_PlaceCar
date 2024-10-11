import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { CompteService, ParkingService, PaysService, ReadPaysDTO, ReservationService, TrensactionService, UserService } from 'src/app/main/api/generated';
import { AuthService } from 'src/app/main/service/auth.service';

@Component({
    templateUrl: './transaction.component.html',
    providers: [MessageService]
})
export class TransactionComponent implements OnInit {

    transactions: any[] = [];
    loading = true;
    dateSelectionnee = new Date();

    @BlockUI() blockUI: NgBlockUI;
    constructor(private layoutService: LayoutService, 
        private formBuilder: FormBuilder, 
        private transactionService: TrensactionService,
        private messageService: MessageService,
        private activatedRoute: ActivatedRoute,
        public authService: AuthService,
        private router: Router
    ) {
    }

    ngOnInit() {

        if(!this.authService.isLoggedIn()) {
            return;
        }
        
        this.rechercher();
    }

    rechercher() {

        if( this.authService.getUser()['role'].toLowerCase() === 'client') {
            this.transactionService.apiTransactionsClientClientIdGet(this.authService.getUser()['nameIdentifier']).subscribe(
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
            );
        }
        else if( this.authService.getUser()['role'].toLowerCase() === 'admin' || 
        this.authService.getUser()['role'].toLowerCase() === 'superadmin') {
            this.transactionService.apiTransactionsPlaceCarGet(new Date(this.dateSelectionnee.getTime() + (24 * 3600 * 1000)).toISOString()).subscribe(
                {
                    next : (result) => {
                        console.log(result);

                        this.transactions = [];

                        for (let index = 0; index < result.length; index++) {
                            const element = result[index];
                            
                            element['transactionDate'] = element['transactionDate']?.substring(0, 19).replace('T', ' ');
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
            );
        }
    }

    onDateDebutChange(date: Date) {
        this.dateSelectionnee = date;
        console.log(this.dateSelectionnee);

        this.rechercher();
    }
}
