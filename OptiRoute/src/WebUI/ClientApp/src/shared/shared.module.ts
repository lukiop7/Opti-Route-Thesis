import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkerDetailsComponent } from './components/marker-details/marker-details.component';



@NgModule({
  declarations: [
    MarkerDetailsComponent
  ],
  exports: [
    MarkerDetailsComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
