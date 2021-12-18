import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkerDetailsComponent } from './components/marker-details/marker-details.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {TimepickerModule} from 'ngx-bootstrap/timepicker';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RouteDetailsComponent } from './components/route-details/route-details.component';



@NgModule({
  declarations: [
    MarkerDetailsComponent,
    RouteDetailsComponent
  ],
    exports: [
        MarkerDetailsComponent,
        RouteDetailsComponent
    ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    TimepickerModule.forRoot(),
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
