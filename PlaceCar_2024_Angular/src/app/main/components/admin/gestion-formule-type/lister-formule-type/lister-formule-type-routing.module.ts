import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerFormuleTypeComponent } from './lister-formule-type.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerFormuleTypeComponent }
	])],
	exports: [RouterModule]
})
export class ListerFormuleTypeRoutingModule { }
