import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { InformationsComponent } from './informations.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: InformationsComponent }
	])],
	exports: [RouterModule]
})
export class InformationsRoutingModule { }
