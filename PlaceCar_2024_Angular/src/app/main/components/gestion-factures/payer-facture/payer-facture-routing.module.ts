import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PayerFactureComponent } from './payer-facture.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: PayerFactureComponent }
	])],
	exports: [RouterModule]
})
export class PayerFactureRoutingModule { }
