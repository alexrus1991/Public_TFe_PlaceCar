import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ChangerParkingEmployeComponent } from './changer-parking-employe.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ChangerParkingEmployeComponent }
	])],
	exports: [RouterModule]
})
export class ChangerParkingEmployeRoutingModule { }
