import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailParkingRoutingModule } from './detail-parking-routing.module';
import { ChartModule } from 'primeng/chart'
import { DetailParkingComponent } from './detail-parking.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { BlockUIModule } from 'ng-block-ui';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@NgModule({
	imports: [
		CommonModule,
		DetailParkingRoutingModule,
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
		ConfirmDialogModule,
		BlockUIModule.forRoot()
	],
	declarations: [DetailParkingComponent]
})
export class DetailParkingModule { }
