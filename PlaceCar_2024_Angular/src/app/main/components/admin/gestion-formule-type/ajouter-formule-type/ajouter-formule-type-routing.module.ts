import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AjouterFormuleTypeComponent } from './ajouter-formule-type.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterFormuleTypeComponent }
	])],
	exports: [RouterModule]
})
export class AjouterFormuleTypeRoutingModule { }
