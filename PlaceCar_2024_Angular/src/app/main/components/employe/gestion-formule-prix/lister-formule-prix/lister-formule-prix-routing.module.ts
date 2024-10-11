import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ListerFormulePrixComponent } from './lister-formule-prix.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: ListerFormulePrixComponent }
	])],
	exports: [RouterModule]
})
export class ListerFormulePrixRoutingModule { }
