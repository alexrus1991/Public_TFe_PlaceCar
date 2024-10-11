import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AjouterAdminRoutingModule } from './ajouter-admin-routing.module';
import { ChartModule } from 'primeng/chart'
import { AjouterAdminComponent } from './ajouter-admin.component';
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
import { StepsModule } from 'primeng/steps';

@NgModule({
	imports: [
		CommonModule,
		AjouterAdminRoutingModule,
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
		StepsModule,
		BlockUIModule.forRoot()
	],
	declarations: [AjouterAdminComponent]
})
export class AjouterAdminModule { }
