import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerTransactionRoutingModule } from './lister-transaction-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerTransactionComponent } from './lister-transaction.component';

@NgModule({
	imports: [
		CommonModule,
		ListerTransactionRoutingModule,
		ChartModule
	],
	declarations: [ListerTransactionComponent]
})
export class ListerTransactionModule { }
