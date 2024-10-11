import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangerParkingEmployeRoutingModule } from './changer-parking-employe-routing.module';
import { ChartModule } from 'primeng/chart'
import { ChangerParkingEmployeComponent } from './changer-parking-employe.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
	imports: [
		CommonModule,
		ChangerParkingEmployeRoutingModule,
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
		PasswordModule,
		BlockUIModule.forRoot()
	],
	declarations: [ChangerParkingEmployeComponent]
})
export class ChangerParkingEmployeModule { }
