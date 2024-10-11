import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Subscription, debounceTime } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { PaysService, ReadPaysDTO } from 'src/app/main/api/generated';
import { CountryService } from 'src/app/main/service/country.service';

@Component({
    templateUrl: './lister-pays.component.html',
    providers: [MessageService]
})
export class ListerPaysComponent implements OnInit, OnDestroy, AfterViewInit {

    paysForm: FormGroup;
    loading: boolean = false;
    @ViewChild('filter') filter!: ElementRef;
    @BlockUI() blockUI: NgBlockUI;
    afficherDialogAjouterPays = false;
    pays: ReadPaysDTO[] = [];
    countries: any[] = [];

    subscription: Subscription;
    constructor(private layoutService: LayoutService, 
        private countryService: CountryService,
        private formBuilder: FormBuilder, 
        private messageService: MessageService,
        private paysService: PaysService) {
        /*this.subscription = this.layoutService.configUpdate$
            .pipe(debounceTime(25))
            .subscribe((config) => {
                this.initCharts();
            });*/

            this.countryService.getCountries().then(countries => {
                this.countries = countries;
            });
   
    }

    ngOnInit() {

        this.paysForm = this.formBuilder.group({
            nomPays: ['', [Validators.required]],
          });

          this.rechercher();

          
    }

    ngAfterViewInit(): void {

    }

    ngOnDestroy() {
        /*if (this.subscription) {
            this.subscription.unsubscribe();
        }*/
    }

    rechercher() {
        this.loading = true;
                  
            this.paysService.apiPaysGet().subscribe({
                next : (result: ReadPaysDTO[]) => {
                    console.log(result);
                    this.pays = [];
                    for (let index = 0; index < result.length; index++) {
                        const element = result[index];
                        
                        let array = this.countries.filter((c) => c.name === element['payS_Nom']);
                        if(array && array.length > 0) {
                            element['code'] = array[0].code;
                        }

                        this.pays.push(element);
                    }
                    this.loading = false;
                },
                error : (err) => {
                    this.loading = false;
                    //this.errorMessage = err.error ? err.error.message ?? err.message : err.message;
                }
            });    
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
        this.filter.nativeElement.value = '';
    }
    
    
    ajouter() {     
        this.paysForm.markAllAsTouched();
        if(this.paysForm.valid) {

            //this.blockUI.start('Opération en cours ...');
            this.paysService.apiPaysPost(this.paysForm.value.nomPays).subscribe(
                {
                    next : (result) => {
                        console.log(result);
                        this.afficherDialogAjouterPays = false;
                        //this.blockUI.stop();
                        this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                        
                        this.rechercher();
                    },
                    error : (err) => {
                        //this.blockUI.stop();
                        console.log('err');
                        console.log(err);

                        if(err['statusText']?.toLowerCase() === 'ok') {
                            this.afficherDialogAjouterPays = false;
                            this.messageService.add({ severity: 'info', summary: 'Information', detail: 'Ajout effectué avec succès' });
                        }
                        else {
                            this.messageService.add({ severity: 'error', summary: 'Information', detail: err.error ?? err.message});

                        }
                    }
                }
            );
        }
    }

    ajouterPays() {
        this.afficherDialogAjouterPays = true;
    }

    annuler() {
        this.afficherDialogAjouterPays = false;
    }
}
