import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkerDetailsComponent } from './components/marker-details/marker-details.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {TimepickerModule} from 'ngx-bootstrap/timepicker';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RouteDetailsComponent } from './components/route-details/route-details.component';
import { LoaderComponent } from './components/loader/loader.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';


@NgModule({
  declarations: [
    MarkerDetailsComponent,
    LoaderComponent,
    RouteDetailsComponent
  ],
    exports: [
        MarkerDetailsComponent,
        RouteDetailsComponent,
        LoaderComponent
    ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    TimepickerModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule
  ]
})
export class SharedModule { }
