import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-employe', loadChildren: () => import('./lister-employe/lister-employe.module').then(m => m.ListerEmployeModule) },
        { path: 'ajouter-employe', loadChildren: () => import('./ajouter-employe/ajouter-employe.module').then(m => m.AjouterEmployeModule) },
        { path: 'ajouter-admin', loadChildren: () => import('./ajouter-admin/ajouter-admin.module').then(m => m.AjouterAdminModule) },
        { path: 'changer-parking-employe', loadChildren: () => import('./changer-parking-employe/changer-parking-employe.module').then(m => m.ChangerParkingEmployeModule) },
        //{ path: 'changer-parking-employe/:idEmploye', loadChildren: () => import('./changer-parking-employe/changer-parking-employe.module').then(m => m.ChangerParkingEmployeModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionEmployeRoutingModule { }
