import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkerDetailsComponent } from './components/marker-details/marker-details.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {TimepickerModule} from 'ngx-bootstrap/timepicker';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';



@NgModule({
  declarations: [
    MarkerDetailsComponent
  ],
  exports: [
    MarkerDetailsComponent
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
