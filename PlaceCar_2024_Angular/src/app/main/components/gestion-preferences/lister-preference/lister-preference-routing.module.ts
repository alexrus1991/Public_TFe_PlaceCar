import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerPreferenceComponent } from './lister-preference.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerPreferenceComponent }
	])],
	exports: [RouterModule]
})
export class ListerPreferenceRoutingModule { }
