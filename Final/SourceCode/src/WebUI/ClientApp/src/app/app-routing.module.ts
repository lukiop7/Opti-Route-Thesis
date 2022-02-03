import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {MapLayoutComponent} from './map-layout/map-layout.component';
import { BenchmarkComponent } from './benchmark/benchmark.component';
import { BenchmarkResultsComponent } from './benchmark-results/benchmark-results.component';

export const routes: Routes = [

  { path: '', component: MapLayoutComponent, pathMatch: 'full' },
  { path: 'benchmark', component: BenchmarkComponent},
  { path: 'benchmark-results', component: BenchmarkResultsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
