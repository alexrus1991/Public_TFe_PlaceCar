import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-pays', loadChildren: () => import('./lister-pays/lister-pays.module').then(m => m.ListerPaysModule) },
        { path: 'ajouter-pays', loadChildren: () => import('./ajouter-pays/ajouter-pays.module').then(m => m.AjouterPaysModule) },
        { path: 'ajouter-pays/:idPays', loadChildren: () => import('./ajouter-pays/ajouter-pays.module').then(m => m.AjouterPaysModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionPaysRoutingModule { }
