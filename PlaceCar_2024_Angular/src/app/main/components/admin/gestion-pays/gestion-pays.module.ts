import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionPaysRoutingModule } from './gestion-pays-routing.module';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { RatingModule } from 'primeng/rating';
import { ButtonModule } from 'primeng/button';
import { SliderModule } from 'primeng/slider';
import { InputTextModule } from 'primeng/inputtext';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { RippleModule } from 'primeng/ripple';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { DropdownModule } from 'primeng/dropdown';
import { ToastModule } from 'primeng/toast';

@NgModule({
	imports: [
		CommonModule,
		GestionPaysRoutingModule
	]
})
export class GestionPaysModule { }
