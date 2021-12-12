import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpParams} from '@angular/common/http';
import {LatLng} from 'leaflet';
import {catchError, map, tap} from 'rxjs/operators';
import {Observable, throwError} from 'rxjs';
import {IDistDur, OsrmTableResponse} from '../../shared/models/osrmTableResponse';

@Injectable({
  providedIn: 'root'
})
export class OsrmService {

  private TABLE_API_SERVER = 'https://router.project-osrm.org/table/v1/driving/';

  constructor(private _httpClient: HttpClient) {
  }

  getDistancesAndDurationsTable(coordinates: LatLng[]): Observable<IDistDur> {
    let params: HttpParams = new HttpParams();
    params = params.append('annotations', 'distance,duration');
    console.log(this.TABLE_API_SERVER + coordinates.map(({lng, lat}) => `${lng},${lat}`).join(';'));
    return this._httpClient.get(this.TABLE_API_SERVER + coordinates.map(({lng, lat}) => `${lng},${lat}`).join(';'), {
      params: params
    }).pipe(
      map((data: OsrmTableResponse) => {
      return {
        distances: data.distances,
        durations: data.durations,
      };
    }),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}
