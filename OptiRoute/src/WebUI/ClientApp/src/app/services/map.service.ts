import {Injectable} from '@angular/core';
import {MapCustomer} from '../../shared/models/mapCustomer';
import {Customer} from '../../shared/models/customer';
import {LatLng, Marker, Polygon} from 'leaflet';
import {removeItem} from '../../shared/utils/removeItem';
import {Observable, Subject} from 'rxjs';
import {CustomerViewModel, CVRPTWClient, DepotViewModel, ProblemViewModel, SolutionViewModel} from '../web-api-client';
import {OsrmService} from './osrm.service';
import {IDistDur} from '../../shared/models/osrmTableResponse';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  private _mapCustomers: MapCustomer[] = [];
  private _markersSubject = new Subject<Marker[]>();
  private _customersSubject = new Subject<Customer[]>();
  private _pathsSubject = new Subject<LatLng[][]>();

  constructor(private _vrptwClient: CVRPTWClient, private _osrmService: OsrmService) {
  }

  getCustomers(): Observable<Customer[]> {
    return this._customersSubject.asObservable();
  }

  getMarkers(): Observable<Marker[]> {
    return this._markersSubject.asObservable();
  }

  getPaths(): Observable<LatLng[][]> {
    return this._pathsSubject.asObservable();
  }

  addMarker(marker: Marker) {
    const customer: Customer = {
      date: new Date(),
      lat: marker.getLatLng().lat,
      lng: marker.getLatLng().lng,
    };
    const newItem: MapCustomer = {customer: customer, marker: marker};
    this._mapCustomers.push(newItem);
    this._customersSubject.next(this._mapCustomers.map(c => c.customer));
    this._markersSubject.next(this._mapCustomers.map(c => c.marker));
  }

  removeCustomer(customer: Customer) {
    const itemToDelete = this._mapCustomers.find(c => c.customer === customer);
    removeItem(this._mapCustomers, itemToDelete);
    this._customersSubject.next(this._mapCustomers.map(c => c.customer));
    this._markersSubject.next(this._mapCustomers.map(c => c.marker));
  }

  async connectMarkers() {
    const coordinated = this._mapCustomers.map(c => c.marker.getLatLng());
    const distDur = await this._osrmService.getDistancesAndDurationsTable(coordinated).toPromise();
    const customers: CustomerViewModel[] = [];
    let time = 0;
    for (let i = 1; i < coordinated.length; i++) {
      customers.push(CustomerViewModel.fromJS({
        id: i,
        x: Math.floor(coordinated[i].lng),
        y: Math.floor(coordinated[i].lat),
        demand: 1,
        readyTime: time,
        dueDate: time + 600,
        serviceTime: 300
      }));
      time = time + 600;
    }
    const problem = ProblemViewModel.fromJS({
      vehicles: 3,
      capacity: 3,
      depot: DepotViewModel.fromJS({
        id: 0,
        x: Math.floor(coordinated[0].lng),
        y: Math.floor(coordinated[0].lat),
        dueDate: (coordinated.length - 1) * 600
      }),
      customers: customers,
      distances: distDur.distances,
      durations: distDur.durations
    });
    const solution = await this._vrptwClient.getSolution(problem).toPromise();
    const routes: LatLng[][] = new Array<Array<LatLng>>();
    if (solution.feasible) {
      for (let i = 0; i < solution.routes.length; i++) {
        const route: LatLng[] = new Array<LatLng>();
        route.push(coordinated[0]);
        for (let j = 0; j < solution.routes[i].customers.length; j++) {
          route.push(coordinated[solution.routes[i].customers[j]]);
        }
        route.push(coordinated[0]);
        routes.push(route);
      }
      console.log(routes);
      this._pathsSubject.next(routes);
    }
  }
}
