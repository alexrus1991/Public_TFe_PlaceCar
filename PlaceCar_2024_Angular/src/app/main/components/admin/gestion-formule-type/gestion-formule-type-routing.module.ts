import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-formule-type', loadChildren: () => import('./lister-formule-type/lister-formule-type.module').then(m => m.ListerFormuleTypeModule) },
        { path: 'ajouter-formule-type', loadChildren: () => import('./ajouter-formule-type/ajouter-formule-type.module').then(m => m.AjouterFormuleTypeModule) },
        { path: 'ajouter-formule-type/:idFormuleType', loadChildren: () => import('./ajouter-formule-type/ajouter-formule-type.module').then(m => m.AjouterFormuleTypeModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionFormuleTypeRoutingModule { }
