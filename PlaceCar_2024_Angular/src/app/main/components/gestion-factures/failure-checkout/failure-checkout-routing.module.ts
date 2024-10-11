import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FailureCheckoutComponent } from './failure-checkout.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: FailureCheckoutComponent }
	])],
	exports: [RouterModule]
})
export class FailureCheckoutRoutingModule { }
