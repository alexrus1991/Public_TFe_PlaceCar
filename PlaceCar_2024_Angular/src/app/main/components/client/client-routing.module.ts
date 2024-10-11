import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'informations', data: { breadcrumb: 'Button' }, loadChildren: () => import('./informations/informations.module').then(m => m.InformationsModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class ClientRoutingModule { }
