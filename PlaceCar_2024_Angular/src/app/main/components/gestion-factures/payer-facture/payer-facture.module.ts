import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PayerFactureRoutingModule } from './payer-facture-routing.module';
import { ChartModule } from 'primeng/chart'
import { PayerFactureComponent } from './payer-facture.component';
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
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CheckboxModule } from 'primeng/checkbox';

@NgModule({
	imports: [
		CommonModule,
		PayerFactureRoutingModule,
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
		ConfirmDialogModule,
		DialogModule,
		CheckboxModule,
		BlockUIModule.forRoot()
	],
	declarations: [PayerFactureComponent]
})
export class PayerFactureModule { }
