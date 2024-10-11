import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerTransactionComponent } from './lister-transaction.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerTransactionComponent }
	])],
	exports: [RouterModule]
})
export class ListerTransactionRoutingModule { }
