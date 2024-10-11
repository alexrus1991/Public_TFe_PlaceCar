import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerEmployeRoutingModule } from './lister-employe-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerEmployeComponent } from './lister-employe.component';
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
import { DialogModule } from 'primeng/dialog';

@NgModule({
	imports: [
		CommonModule,
		ListerEmployeRoutingModule,
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
		DialogModule,
		BlockUIModule.forRoot()
	],
	declarations: [ListerEmployeComponent]
})
export class ListerEmployeModule { }
