import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'detail-parking', loadChildren: () => import('./detail-parking/detail-parking.module').then(m => m.DetailParkingModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionParkingRoutingModule { }
