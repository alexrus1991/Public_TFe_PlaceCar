import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerFormulePrixRoutingModule } from './lister-formule-prix-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerFormulePrixComponent } from './lister-formule-prix.component';
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
		ListerFormulePrixRoutingModule,
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
	declarations: [ListerFormulePrixComponent]
})
export class ListerFormulePrixModule { }
