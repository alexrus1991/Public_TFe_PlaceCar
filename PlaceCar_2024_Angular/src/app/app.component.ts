import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { environment } from 'src/environments/environment';
import { AuthService } from './main/service/auth.service';
import { Router } from '@angular/router';
import { LayoutService } from './layout/service/app.layout.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

    constructor(private primengConfig: PrimeNGConfig,
        public layoutService: LayoutService,
        private authService: AuthService,
        private router: Router
    ) { 
        console.log('AppComponent, logged in: ' + this.authService.isLoggedIn());
        /*if(!this.authService.isLoggedIn()) {
            this.layoutService.state.staticMenuDesktopInactive = false;
            this.layoutService.onMenuToggle();
            this.router.navigateByUrl('/');
        } 
        else {
            this.layoutService.state.staticMenuDesktopInactive = true;
            this.layoutService.onMenuToggle();
        }*/
    }

    ngOnInit() {
        this.primengConfig.ripple = true;
        console.log("Running in production mode ? " + environment.production);
    }
}
