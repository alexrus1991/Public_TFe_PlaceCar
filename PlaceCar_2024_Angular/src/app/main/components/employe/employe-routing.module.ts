import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'informations', data: { breadcrumb: 'Button' }, loadChildren: () => import('./informations/informations.module').then(m => m.InformationsModule) },
        { path: 'gestion-formule-prix', loadChildren: () => import('./gestion-formule-prix/gestion-formule-prix.module').then(m => m.GestionFormulePrixModule) },
        { path: 'gestion-employe', loadChildren: () => import('./gestion-employe/gestion-employe.module').then(m => m.GestionEmployeModule) },
        { path: 'gestion-parking', loadChildren: () => import('./gestion-parking/gestion-parking.module').then(m => m.GestionParkingModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class EmployeRoutingModule { }
