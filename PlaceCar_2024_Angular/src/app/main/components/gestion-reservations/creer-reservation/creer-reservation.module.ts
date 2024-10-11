import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreerReservationRoutingModule } from './creer-reservation-routing.module';
import { ChartModule } from 'primeng/chart'
import { CreerReservationComponent } from './creer-reservation.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { BrowserModule } from '@angular/platform-browser';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { BlockUIModule } from 'ng-block-ui';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { PasswordModule } from 'primeng/password';
import { DividerModule } from 'primeng/divider';
import { StyleClassModule } from 'primeng/styleclass';
import { PanelModule } from 'primeng/panel';
import { AutoCompleteModule } from 'primeng/autocomplete';

@NgModule({
	imports: [
		CommonModule,
        CreerReservationRoutingModule,
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
		BlockUIModule.forRoot()
	],
	declarations: [CreerReservationComponent]
})
export class CreerReservationModule { }
