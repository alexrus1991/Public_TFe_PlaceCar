import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionReservationsRoutingModule } from './gestion-reservations-routing.module';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
	imports: [
		CommonModule,
		GestionReservationsRoutingModule,
	]
})
export class GestionReservationsModule { }
