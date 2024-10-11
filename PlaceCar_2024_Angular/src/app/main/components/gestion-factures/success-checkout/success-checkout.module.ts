import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SuccessCheckoutComponent } from './success-checkout.component';
import { SuccessCheckoutRoutingModule } from './success-checkout-routing.module';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
    imports: [
        CommonModule,
        SuccessCheckoutRoutingModule,
        FormsModule,
		ReactiveFormsModule,
		ButtonModule,
		BlockUIModule.forRoot()
    ],
	declarations: [SuccessCheckoutComponent]
})
export class SuccessCheckoutModule { }
