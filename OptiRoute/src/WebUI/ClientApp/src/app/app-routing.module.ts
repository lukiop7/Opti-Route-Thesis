import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import {MapLayoutComponent} from './map-layout/map-layout.component';
import { BenchmarkComponent } from './benchmark/benchmark.component';
import { BenchmarkResultsComponent } from './benchmark-results/benchmark-results.component';

export const routes: Routes = [

  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'map', component: MapLayoutComponent},
  { path: 'benchmark', component: BenchmarkComponent},
  { path: 'benchmark-results', component: BenchmarkResultsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
