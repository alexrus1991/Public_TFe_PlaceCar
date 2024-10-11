import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-formule-prix', loadChildren: () => import('./lister-formule-prix/lister-formule-prix.module').then(m => m.ListerFormulePrixModule) },
        { path: 'ajouter-formule-prix', loadChildren: () => import('./ajouter-formule-prix/ajouter-formule-prix.module').then(m => m.AjouterFormulePrixModule) },
        { path: 'ajouter-formule-prix/:idFormulePrix', loadChildren: () => import('./ajouter-formule-prix/ajouter-formule-prix.module').then(m => m.AjouterFormulePrixModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionFormulePrixRoutingModule { }
