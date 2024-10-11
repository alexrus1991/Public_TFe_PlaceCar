import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerParkingComponent } from './lister-parking.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerParkingComponent }
	])],
	exports: [RouterModule]
})
export class ListerParkingRoutingModule { }
