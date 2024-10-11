import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-transaction', loadChildren: () => import('./lister-transaction/lister-transaction.module').then(m => m.ListerTransactionModule) },
        { path: 'ajouter-transaction', loadChildren: () => import('./ajouter-transaction/ajouter-transaction.module').then(m => m.AjouterTransactionModule) },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionTransactionsRoutingModule { }
