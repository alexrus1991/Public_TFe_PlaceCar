import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreerReservationComponent } from './creer-reservation.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: CreerReservationComponent }
	])],
	exports: [RouterModule]
})
export class CreerReservationRoutingModule { }
