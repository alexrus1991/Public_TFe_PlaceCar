import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerPaysComponent } from './lister-pays.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerPaysComponent }
	])],
	exports: [RouterModule]
})
export class ListerPaysRoutingModule { }
