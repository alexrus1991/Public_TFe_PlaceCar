import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AjouterEmployeComponent } from './ajouter-employe.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterEmployeComponent },
	])],
	exports: [RouterModule]
})
export class AjouterEmployeRoutingModule { }
