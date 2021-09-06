import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TestRoutingModule } from './test-routing.module';
import { TestModuleComponent } from './test-module/test-module.component';
import { TestCRUDComponent } from './test-crud/test-crud.component';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';


@NgModule({
  declarations: [
    TestModuleComponent,
    TestCRUDComponent
  ],
  imports: [
    CommonModule,
    TestRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    LeafletModule
  ]
})
export class TestModule { }
