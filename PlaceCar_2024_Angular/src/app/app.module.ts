import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutModule } from './layout/layout.module';
import { ApiModule, Configuration, ConfigurationParameters } from './main/api/generated';
import { NotfoundComponent } from './main/components/notfound/notfound.component';
import { CountryService } from './main/service/country.service';
import { CustomerService } from './main/service/customer.service';
import { EventService } from './main/service/event.service';
import { IconService } from './main/service/icon.service';
import { NodeService } from './main/service/node.service';
import { PhotoService } from './main/service/photo.service';
import { ProductService } from './main/service/product.service';
import { TokenInterceptor } from "./main/service/token.interceptor";


export function apiConfigFactory(): Configuration {
    const params: ConfigurationParameters = {
      basePath: environment.apiUrl,
    };
    return new Configuration(params);
  }

@NgModule({
    declarations: [AppComponent, NotfoundComponent],
    imports: [
        AppRoutingModule,
        ApiModule.forRoot(apiConfigFactory),
        AppLayoutModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        },
        {
            provide: LocationStrategy,
            useClass: PathLocationStrategy
        },
        CountryService, CustomerService, EventService, IconService, NodeService,
        PhotoService, ProductService
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
