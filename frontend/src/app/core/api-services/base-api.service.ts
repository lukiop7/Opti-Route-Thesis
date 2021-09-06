import { HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

export declare const BAD_REQUEST = 400;
export abstract class BaseApiService {

  protected getHeaders(options?: { [name: string]: any }): HttpHeaders {
    return new HttpHeaders({
      ...{ 'Content-Type': 'application/json' },
      ...options
    });
  }

  protected handleValidationError(error: any): Observable<never> {
    if (error.error instanceof ErrorEvent) {
      return throwError(error);
    }

    if (error.status === BAD_REQUEST) {
      console.log(error);
    }

    return throwError(error);
  }
}
