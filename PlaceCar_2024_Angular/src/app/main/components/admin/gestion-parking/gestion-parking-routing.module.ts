import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-parking', loadChildren: () => import('./lister-parking/lister-parking.module').then(m => m.ListerParkingModule) },
        { path: 'ajouter-parking', loadChildren: () => import('./ajouter-parking/ajouter-parking.module').then(m => m.AjouterParkingModule) },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionParkingRoutingModule { }
