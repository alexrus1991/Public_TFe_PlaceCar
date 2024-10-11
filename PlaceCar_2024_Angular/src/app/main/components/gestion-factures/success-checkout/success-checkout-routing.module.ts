import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SuccessCheckoutComponent } from './success-checkout.component';

@NgModule({
	imports: [RouterModule.forChild([
		{ path: '', component: SuccessCheckoutComponent }
	])],
	exports: [RouterModule]
})
export class SuccessCheckoutRoutingModule { }
