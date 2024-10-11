import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AjouterFormulePrixComponent } from './ajouter-formule-prix.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterFormulePrixComponent }
	])],
	exports: [RouterModule]
})
export class AjouterFormulePrixRoutingModule { }
