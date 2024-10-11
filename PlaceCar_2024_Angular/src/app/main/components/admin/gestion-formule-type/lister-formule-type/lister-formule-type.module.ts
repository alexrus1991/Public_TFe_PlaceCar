import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerFormuleTypeRoutingModule } from './lister-formule-type-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerFormuleTypeComponent } from './lister-formule-type.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { BlockUIModule } from 'ng-block-ui';
import { CalendarModule } from 'primeng/calendar';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DialogModule } from 'primeng/dialog';

@NgModule({
	imports: [
		CommonModule,
		ListerFormuleTypeRoutingModule,
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
	declarations: [ListerFormuleTypeComponent]
})
export class ListerFormuleTypeModule { }
