import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerParkingRoutingModule } from './lister-parking-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerParkingComponent } from './lister-parking.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ToastModule } from 'primeng/toast';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
	imports: [
		CommonModule,
		ListerParkingRoutingModule,
		FormsModule,
		ReactiveFormsModule,
		TableModule,
		InputTextModule,
		ButtonModule,
		CalendarModule,
		DropdownModule,
		InputMaskModule,
		InputNumberModule,
		InputTextareaModule,
		InputTextModule,
		ToastModule,
		BlockUIModule.forRoot()
	],
	declarations: [ListerParkingComponent]
})
export class ListerParkingModule { }
