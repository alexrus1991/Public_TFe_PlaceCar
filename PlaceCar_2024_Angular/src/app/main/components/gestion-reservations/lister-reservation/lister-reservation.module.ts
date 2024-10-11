import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerReservationRoutingModule } from './lister-reservation-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerReservationComponent } from './lister-reservation.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { BrowserModule } from '@angular/platform-browser';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { BlockUIModule } from 'ng-block-ui';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { StyleClassModule } from 'primeng/styleclass';
import { DividerModule } from 'primeng/divider';
import { PanelModule } from 'primeng/panel';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { PickListModule } from 'primeng/picklist';
import { ListboxModule } from 'primeng/listbox';
import { DialogModule } from 'primeng/dialog';
import { PasswordModule } from 'primeng/password';

@NgModule({
	imports: [
		CommonModule,
		ListerReservationRoutingModule,
		FormsModule,
		ReactiveFormsModule,
		TableModule,
		InputTextModule,
		ButtonModule,
		DropdownModule,
		InputMaskModule,
		InputNumberModule,
		InputTextareaModule,
		InputTextModule,
		ToastModule,
		DividerModule,
        StyleClassModule,
        ChartModule,
        PanelModule,
		AutoCompleteModule,
		CalendarModule,
		OverlayPanelModule,
		PickListModule,
		ListboxModule,
		ToastModule,
		ConfirmDialogModule,
		DialogModule,
		PasswordModule,
		BlockUIModule.forRoot()
	],
	declarations: [ListerReservationComponent]
})
export class ListerReservationModule { }
