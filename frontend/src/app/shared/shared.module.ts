import { TestApiService } from './services/test/test-api.service';
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { TestService } from './services/test/test.service';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports:[ReactiveFormsModule],
  providers: [DatePipe]
})
export class SharedModule {
  static forRoot(): { ngModule: any; providers: any[] } {
    return {
      ngModule: SharedModule,
      providers: [TestService,TestApiService],
    };
  }
 }
