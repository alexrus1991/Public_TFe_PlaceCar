import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerFactureRoutingModule } from './lister-facture-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerFactureComponent } from './lister-facture.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { BrowserModule } from '@angular/platform-browser';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
	imports: [
		CommonModule,
		ListerFactureRoutingModule,
		ReactiveFormsModule,
		FormsModule,
		TableModule,
		InputTextModule,
		DropdownModule,
		ButtonModule,
		ToastModule,
		ConfirmDialogModule,
		BlockUIModule.forRoot()
	],
	declarations: [ListerFactureComponent]
})
export class ListerFactureModule { }
