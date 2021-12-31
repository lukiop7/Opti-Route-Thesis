import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {LeafletModule} from '@asymmetrik/ngx-leaflet';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { ApiAuthorizationModule } from 'api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { TokenComponent } from './token/token.component';
import { MapComponent } from './map/map.component';
import {SharedModule} from '../shared/shared.module';
import { MapSidebarComponent } from './map-sidebar/map-sidebar.component';
import { MapLayoutComponent } from './map-layout/map-layout.component';
import {MapService} from './services/map.service';
import {OsrmService} from './services/osrm.service';
import { MapSidebarCustomersComponent } from './map-sidebar/map-sidebar-customers/map-sidebar-customers.component';
import { MapSidebarDepotComponent } from './map-sidebar/map-sidebar-depot/map-sidebar-depot.component';
import { MapSidebarSolutionComponent } from './map-sidebar/map-sidebar-solution/map-sidebar-solution.component';
import { MapSidebarGettingStartedComponent } from './map-sidebar/map-sidebar-getting-started/map-sidebar-getting-started.component';
import {MatStepperModule} from '@angular/material/stepper';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {TimepickerModule} from 'ngx-bootstrap';
import { MapSidebarProblemInfoComponent } from './map-sidebar/map-sidebar-problem-info/map-sidebar-problem-info.component';
import { LoaderService } from './services/loader.service';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { AppErrorHandler } from './error-handler/error-handler';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    TokenComponent,
    MapComponent,
    MapSidebarComponent,
    MapLayoutComponent,
    MapSidebarCustomersComponent,
    MapSidebarDepotComponent,
    MapSidebarSolutionComponent,
    MapSidebarGettingStartedComponent,
    MapSidebarProblemInfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ModalModule.forRoot(),
    LeafletModule,
    SharedModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatProgressSpinnerModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut:3000,
      progressBar:true,
    }),
    TimepickerModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    {provide: ErrorHandler, useClass: AppErrorHandler},
    MapService,
    OsrmService,
    LoaderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
