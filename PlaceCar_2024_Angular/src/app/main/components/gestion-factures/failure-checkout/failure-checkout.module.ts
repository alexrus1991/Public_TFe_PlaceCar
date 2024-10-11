import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FailureCheckoutComponent } from './failure-checkout.component';
import { FailureCheckoutRoutingModule } from './failure-checkout-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { BlockUIModule } from 'ng-block-ui';

@NgModule({
    imports: [
        CommonModule,
        FailureCheckoutRoutingModule, 
        FormsModule,
		ReactiveFormsModule,
		ButtonModule,
		BlockUIModule.forRoot()
    ],
	declarations: [FailureCheckoutComponent]
})
export class FailureCheckoutModule { }
