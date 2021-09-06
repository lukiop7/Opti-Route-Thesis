import { TestCRUDComponent } from './test-crud/test-crud.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestModuleComponent } from './test-module/test-module.component';

const routes: Routes = [
  {
    path: 'test/module',
    component: TestModuleComponent,
  },
  {
    path: 'test/form',
    component: TestCRUDComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TestRoutingModule { }
