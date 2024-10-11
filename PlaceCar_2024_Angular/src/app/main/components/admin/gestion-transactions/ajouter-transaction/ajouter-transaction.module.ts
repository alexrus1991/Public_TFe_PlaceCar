import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AjouterTransactionRoutingModule } from './ajouter-transaction-routing.module';
import { ChartModule } from 'primeng/chart'
import { AjouterTransactionComponent } from './ajouter-transaction.component';

@NgModule({
	imports: [
		CommonModule,
		AjouterTransactionRoutingModule,
		ChartModule
	],
	declarations: [AjouterTransactionComponent]
})
export class AjouterTransactionModule { }
