import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import {AjouterAdminComponent } from './ajouter-admin.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterAdminComponent },
	])],
	exports: [RouterModule]
})
export class AjouterAdminRoutingModule { }
