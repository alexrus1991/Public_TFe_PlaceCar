import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InformationsRoutingModule } from './informations-routing.module';
import { ChartModule } from 'primeng/chart'
import { InformationsComponent } from './informations.component';

@NgModule({
	imports: [
		CommonModule,
		InformationsRoutingModule,
		ChartModule
	],
	declarations: [InformationsComponent]
})
export class InformationsModule { }
