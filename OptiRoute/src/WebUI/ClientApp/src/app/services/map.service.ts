import {Injectable} from '@angular/core';
import {MapCustomer} from '../../shared/models/mapCustomer';
import {Customer} from '../../shared/models/customer';
import {LatLng, Marker, Polygon} from 'leaflet';
import {removeItem} from '../../shared/utils/removeItem';
import {Observable, Subject} from 'rxjs';
import {
  CustomerDto,
  CVRPTWClient,
  DepotDto,
  ProblemDto,
} from '../web-api-client';
import {OsrmService} from './osrm.service';
import {IDistDur} from '../../shared/models/osrmTableResponse';
import {FormGroup} from '@angular/forms';

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

  async connectMarkers(data) {
    console.log('siema');
    console.log(data);
    const coordinated = this._mapCustomers.map(c => c.marker.getLatLng());
    console.log(this._mapCustomers);
    const distDur = await this._osrmService.getDistancesAndDurationsTable(coordinated).toPromise();
    const customers: CustomerDto[] = [];
    let time = 0;
    const today = new Date();
    today.setDate(today.getDate() - 1);
    for (let i = 1; i < coordinated.length; i++) {
      console.log(today.toDateString());
      console.log(new Date(today.toDateString() + ' ' + data.customersInfoForm.customersInfo[i - 1].readyTime));
      customers.push(CustomerDto.fromJS({
        id: i,
        x: Math.floor(coordinated[i].lng),
        y: Math.floor(coordinated[i].lat),
        demand: data.customersInfoForm.customersInfo[i - 1].demand,
        readyTime: new Date(today.toDateString() + ' ' + data.customersInfoForm.customersInfo[i - 1].readyTime),
        dueDate: new Date(today.toDateString() + ' ' + data.customersInfoForm.customersInfo[i - 1].dueDate),
        serviceTime: new Date(today.toDateString() + ' ' + data.customersInfoForm.customersInfo[i - 1].serviceTime)
      }));
    }
    const problem = ProblemDto.fromJS({
      vehicles: data.problemInfo.vehicles,
      capacity: data.problemInfo.capacity,
      depot: DepotDto.fromJS({
        id: 0,
        x: Math.floor(coordinated[0].lng),
        y: Math.floor(coordinated[0].lat),
        dueDate: new Date(today.toDateString() + ' ' + data.depotInfo.dueDate),
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
