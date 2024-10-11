import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DetailParkingComponent } from './detail-parking.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: DetailParkingComponent }
	])],
	exports: [RouterModule]
})
export class DetailParkingRoutingModule { }
