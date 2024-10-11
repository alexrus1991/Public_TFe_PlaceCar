import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreerReservationMapComponent } from './creer-reservation-map.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: CreerReservationMapComponent }
	])],
	exports: [RouterModule]
})
export class CreerReservationMapRoutingModule { }
