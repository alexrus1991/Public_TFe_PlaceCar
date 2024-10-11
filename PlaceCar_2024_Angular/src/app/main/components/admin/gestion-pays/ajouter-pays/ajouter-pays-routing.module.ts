import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AjouterPaysComponent } from './ajouter-pays.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterPaysComponent }
	])],
	exports: [RouterModule]
})
export class AjouterPaysRoutingModule { }
