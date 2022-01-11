import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarkerDetailsComponent } from './components/marker-details/marker-details.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouteDetailsComponent } from './components/route-details/route-details.component';
import { LoaderComponent } from './components/loader/loader.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule } from '@angular/material/dialog';
import { DistanceFormattedPipe } from './pipes/distanceFormatted';
import { TimeFormattedPipe } from './pipes/timeFormatted';
import { VisualizerComponent } from './components/visualizer/visualizer.component';


@NgModule({
  declarations: [
    MarkerDetailsComponent,
    LoaderComponent,
    RouteDetailsComponent,
    DistanceFormattedPipe,
    TimeFormattedPipe,
    VisualizerComponent
  ],
  exports: [
    MarkerDetailsComponent,
    RouteDetailsComponent,
    LoaderComponent,
    DistanceFormattedPipe,
    TimeFormattedPipe,
    VisualizerComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    TimepickerModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MatDialogModule,
  ],
  entryComponents:[VisualizerComponent]
})
export class SharedModule { }
