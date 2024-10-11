import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionPreferencesRoutingModule } from './gestion-preferences-routing.module';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
	imports: [
		CommonModule,
		GestionPreferencesRoutingModule,
	]
})
export class GestionPreferencesModule { }
