import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { NotfoundComponent } from './main/components/notfound/notfound.component';
import { LayoutComponent } from "./layout/layout.component";

@NgModule({
    imports: [
        RouterModule.forRoot([
            {
                path: '', component: LayoutComponent,
                children: [
                    { path: '', loadChildren: () => import('./main/components/dashboard/dashboard.module').then(m => m.DashboardModule) },

                    { path: 'admin', loadChildren: () => import('./main/components/admin/admin.module').then(m => m.AdminModule) },
                    { path: 'client', loadChildren: () => import('./main/components/client/client.module').then(m => m.ClientModule) },
                    { path: 'employe', loadChildren: () => import('./main/components/employe/employe.module').then(m => m.EmployeModule) },
                    { path: 'gestion-reservations', loadChildren: () => import('./main/components/gestion-reservations/gestion-reservations.module').then(m => m.GestionReservationsModule) },
                    { path: 'gestion-factures', loadChildren: () => import('./main/components/gestion-factures/gestion-factures.module').then(m => m.GestionFacturesModule) },
                    { path: 'gestion-preferences', loadChildren: () => import('./main/components/gestion-preferences/gestion-preferences.module').then(m => m.GestionPreferencesModule) },
                    { path: 'mon-compte', loadChildren: () => import('./main/components/mon-compte/mon-compte.module').then(m => m.MonCompteModule) },
                    { path: 'transactions', loadChildren: () => import('./main/components/transaction/transaction.module').then(m => m.TransactionModule) },

                    // Pas d'utilisation de ce block
                    { path: 'uikit', loadChildren: () => import('./main/components/uikit/uikit.module').then(m => m.UIkitModule) },                                      
                    { path: 'utilities', loadChildren: () => import('./main/components/utilities/utilities.module').then(m => m.UtilitiesModule) },
                    { path: 'documentation', loadChildren: () => import('./main/components/documentation/documentation.module').then(m => m.DocumentationModule) },
                    { path: 'blocks', loadChildren: () => import('./main/components/primeblocks/primeblocks.module').then(m => m.PrimeBlocksModule) },
                    { path: 'pages', loadChildren: () => import('./main/components/pages/pages.module').then(m => m.PagesModule) }
                ]
            },
            { path: 'auth', loadChildren: () => import('./main/components/auth/auth.module').then(m => m.AuthModule) },
            { path: 'landing', loadChildren: () => import('./main/components/landing/landing.module').then(m => m.LandingModule) },//pas d'utilisation
            { path: 'notfound', component: NotfoundComponent },
            { path: '**', redirectTo: '/notfound' },
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
