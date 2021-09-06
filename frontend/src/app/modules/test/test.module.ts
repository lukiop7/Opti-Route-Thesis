import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TestRoutingModule } from './test-routing.module';
import { TestModuleComponent } from './test-module/test-module.component';
import { TestCRUDComponent } from './test-crud/test-crud.component';


@NgModule({
  declarations: [
    TestModuleComponent,
    TestCRUDComponent
  ],
  imports: [
    CommonModule,
    TestRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class TestModule { }
