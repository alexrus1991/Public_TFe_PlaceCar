import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListerPaysRoutingModule } from './lister-pays-routing.module';
import { ChartModule } from 'primeng/chart'
import { ListerPaysComponent } from './lister-pays.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RatingModule } from 'primeng/rating';
import { ButtonModule } from 'primeng/button';
import { SliderModule } from 'primeng/slider';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { RippleModule } from 'primeng/ripple';
import { MultiSelectModule } from 'primeng/multiselect';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressBarModule } from 'primeng/progressbar';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
	imports: [
		CommonModule,
		ListerPaysRoutingModule,
		FormsModule,
		ReactiveFormsModule,
		TableModule,
		RatingModule,
		ButtonModule,
		SliderModule,
		InputTextModule,
		ToggleButtonModule,
		RippleModule,
		MultiSelectModule,
		DropdownModule,
		ProgressBarModule,
		ToastModule,
		ConfirmDialogModule,
		DialogModule,
		BlockUIModule.forRoot()
	],
	declarations: [ListerPaysComponent]
})
export class ListerPaysModule { }
