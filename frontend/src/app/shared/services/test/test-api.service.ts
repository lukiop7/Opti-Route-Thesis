import { BaseApiService } from '../../../core/api-services/base-api.service';
import { Observable } from 'rxjs';
import { FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { testModel } from '../../models/test.model';


@Injectable({
  providedIn: 'root',
})
@Injectable()
export class TestApiService extends BaseApiService {
  constructor(
    private readonly http: HttpClient,
    private fb: FormBuilder,
    public datepipe: DatePipe,
  ) {
    super();
  }

  private get apiUrl(): string {
    return "https://localhost:44342/";
  }

  createUserAccount(
    name: string,
    age: number
  ): any {
    const credentials = {
      name,
      age,
    };
    return this.http.post<any>(
      "https://localhost:44342/main/Main/create",
      credentials,
      {
        headers: this.getHeaders(),
      }
    );
  }
}
