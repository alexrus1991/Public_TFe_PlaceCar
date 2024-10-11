import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerReservationComponent } from './lister-reservation.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerReservationComponent }
	])],
	exports: [RouterModule]
})
export class ListerReservationRoutingModule { }
