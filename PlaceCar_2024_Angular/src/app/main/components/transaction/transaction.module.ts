import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionRoutingModule } from './transaction-routing.module';
import { ChartModule } from 'primeng/chart'
import { TransactionComponent } from './transaction.component';
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
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';

@NgModule({
	imports: [
		CommonModule,
		TransactionRoutingModule,
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
		DialogModule,
		BlockUIModule.forRoot()
	],
	declarations: [TransactionComponent]
})
export class TransactionModule { }
