import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkerDetailsComponent } from './components/marker-details/marker-details.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [
    MarkerDetailsComponent
  ],
  exports: [
    MarkerDetailsComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule
  ]
})
export class SharedModule { }
