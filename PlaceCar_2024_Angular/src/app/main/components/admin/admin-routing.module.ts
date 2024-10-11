import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'informations', loadChildren: () => import('./informations/informations.module').then(m => m.InformationsModule) },
        { path: 'gestion-employe', loadChildren: () => import('./gestion-employe/gestion-employe.module').then(m => m.GestionEmployeModule) },
        { path: 'gestion-parking', loadChildren: () => import('./gestion-parking/gestion-parking.module').then(m => m.GestionParkingModule) },
        { path: 'gestion-pays', loadChildren: () => import('./gestion-pays/gestion-pays.module').then(m => m.GestionPaysModule) },
        { path: 'gestion-formule-type', loadChildren: () => import('./gestion-formule-type/gestion-formule-type.module').then(m => m.GestionFormuleTypeModule) },
        { path: 'gestion-transactions', loadChildren: () => import('./gestion-transactions/gestion-transactions.module').then(m => m.GestionTransactionsModule) },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
