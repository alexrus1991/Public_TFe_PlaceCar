import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-employe', loadChildren: () => import('./lister-employe/lister-employe.module').then(m => m.ListerEmployeModule) },

        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionEmployeRoutingModule { }
