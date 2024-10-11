import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AjouterTransactionComponent } from './ajouter-transaction.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterTransactionComponent }
	])],
	exports: [RouterModule]
})
export class AjouterTransactionRoutingModule { }
