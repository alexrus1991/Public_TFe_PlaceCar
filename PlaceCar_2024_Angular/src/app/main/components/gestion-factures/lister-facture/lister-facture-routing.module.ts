import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerFactureComponent } from './lister-facture.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerFactureComponent }
	])],
	exports: [RouterModule]
})
export class ListerFactureRoutingModule { }
