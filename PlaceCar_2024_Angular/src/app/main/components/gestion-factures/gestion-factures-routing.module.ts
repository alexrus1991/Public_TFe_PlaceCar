import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-facture', loadChildren: () => import('./lister-facture/lister-facture.module').then(m => m.ListerFactureModule) },
        { path: 'payer-facture', loadChildren: () => import('./payer-facture/payer-facture.module').then(m => m.PayerFactureModule) },
        { path: 'success', loadChildren: () => import('./success-checkout/success-checkout.module').then(m => m.SuccessCheckoutModule) },
        { path: 'failure', loadChildren: () => import('./failure-checkout/failure-checkout.module').then(m => m.FailureCheckoutModule) },

        //{ path: 'failure', loadChildren: () => import('./').then(m => m.ChekcoutModule) },
        //{ path: 'creer-reservation', loadChildren: () => import('./creer-reservation/creer-reservation.module').then(m => m.CreerReservationModule) },
        //{ path: 'creer-reservation-map', loadChildren: () => import('./creer-reservation-map/creer-reservation-map.module').then(m => m.CreerReservationMapModule) },

       // { path: 'creer-utilisateur/:idUtilisateur', loadChildren: () => import('./creer-utilisateur/creer-utilisateur.module').then(m => m.CreerUtilisateurModule) },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionFacturesRoutingModule { }

