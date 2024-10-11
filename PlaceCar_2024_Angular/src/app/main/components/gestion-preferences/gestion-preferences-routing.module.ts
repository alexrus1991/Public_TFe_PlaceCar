import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-preference', loadChildren: () => import('./lister-preference/lister-preference.module').then(m => m.ListerPreferenceModule) },

        //{ path: 'creer-reservation', loadChildren: () => import('./creer-reservation/creer-reservation.module').then(m => m.CreerReservationModule) },
        //{ path: 'creer-reservation-map', loadChildren: () => import('./creer-reservation-map/creer-reservation-map.module').then(m => m.CreerReservationMapModule) },

       // { path: 'creer-utilisateur/:idUtilisateur', loadChildren: () => import('./creer-utilisateur/creer-utilisateur.module').then(m => m.CreerUtilisateurModule) },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionPreferencesRoutingModule { }
