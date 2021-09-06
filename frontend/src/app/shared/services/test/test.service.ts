import { TestApiService } from './test-api.service';
import { BaseApiService } from '../../../core/api-services/base-api.service';
import { DatePipe } from '@angular/common';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root',
})
@Injectable()
export class TestService  {


  constructor(private testApiService: TestApiService, private router: Router) {
  }

  createUserAccount(
    name: string,
    age: number
  ): any {
    return this.testApiService.createUserAccount(name, age).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

}
