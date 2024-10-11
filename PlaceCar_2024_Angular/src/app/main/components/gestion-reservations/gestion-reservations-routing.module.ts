import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'lister-reservation', loadChildren: () => import('./lister-reservation/lister-reservation.module').then(m => m.ListerReservationModule) },
        { path: 'creer-reservation', loadChildren: () => import('./creer-reservation/creer-reservation.module').then(m => m.CreerReservationModule) },
        { path: 'creer-reservation-map', loadChildren: () => import('./creer-reservation-map/creer-reservation-map.module').then(m => m.CreerReservationMapModule) },

       // { path: 'creer-utilisateur/:idUtilisateur', loadChildren: () => import('./creer-utilisateur/creer-utilisateur.module').then(m => m.CreerUtilisateurModule) },
        { path: '**', redirectTo: '/notfound' }
    ])],
    exports: [RouterModule]
})
export class GestionReservationsRoutingModule { }
