import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerEmployeComponent } from './lister-employe.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerEmployeComponent }
	])],
	exports: [RouterModule]
})
export class ListerEmployeRoutingModule { }
