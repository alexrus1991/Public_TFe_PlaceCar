import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AjouterParkingComponent } from './ajouter-parking.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: AjouterParkingComponent }
	])],
	exports: [RouterModule]
})
export class AjouterParkingRoutingModule { }
