import {Injectable} from '@angular/core';
import {MapCustomer} from '../../shared/models/mapCustomer';
import {Customer} from '../../shared/models/customer';
import {LatLng, Marker, Polygon} from 'leaflet';
import {removeItem} from '../../shared/utils/removeItem';
import {BehaviorSubject, Observable, ReplaySubject, Subject} from 'rxjs';
import {
  CustomerDto,
  CVRPTWClient,
  DepotDto,
  ProblemDto,
} from '../web-api-client';
import {OsrmService} from './osrm.service';
import {IDistDur} from '../../shared/models/osrmTableResponse';
import {FormGroup} from '@angular/forms';
import {VrptwSolutionResponse} from '../../shared/models/vrptwSolutionResponse';
import {addHours} from '../../shared/utils/addHours';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  private _mapCustomers: MapCustomer[] = [];
  private _depot: MapCustomer;
  private _markersSubject = new Subject<Marker[]>();
  private _customersSubject = new Subject<Customer[]>();
  private _depotMarkerSubject = new Subject<Marker>();
  private _depotSubject = new Subject<Customer>();
  private _pathsSubject = new ReplaySubject<VrptwSolutionResponse>(1);
  private _viewSubject = new BehaviorSubject<number>(0);
  private _selectedPathSubject = new Subject<number>();

  constructor(private _vrptwClient: CVRPTWClient, private _osrmService: OsrmService) {
  }

  getCustomers(): Observable<Customer[]> {
    return this._customersSubject.asObservable();
  }

  getDepotMarker(): Observable<Marker> {
    return this._depotMarkerSubject.asObservable();
  }

  getDepot(): Observable<Customer> {
    return this._depotSubject.asObservable();
  }

  getMarkers(): Observable<Marker[]> {
    return this._markersSubject.asObservable();
  }

  getPaths(): Observable<VrptwSolutionResponse> {
    return this._pathsSubject.asObservable();
  }

  getView(): Observable<number> {
    return this._viewSubject.asObservable();
  }

  setView(value: number) {
    this._viewSubject.next(value);
  }

  getSelectedPath(): Observable<number> {
    return this._selectedPathSubject.asObservable();
  }

  setSelectedPath(value: number) {
    this._selectedPathSubject.next(value);
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

  addDepot(marker: Marker) {
    const customer: Customer = {
      date: new Date(),
      lat: marker.getLatLng().lat,
      lng: marker.getLatLng().lng,
    };
    const newItem: MapCustomer = {customer: customer, marker: marker};
    this._depot = newItem;
    this._depotMarkerSubject.next(this._depot.marker);
    this._depotSubject.next(this._depot.customer);
  }

  removeCustomer(customer: Customer) {
    const itemToDelete = this._mapCustomers.find(c => c.customer === customer);
    removeItem(this._mapCustomers, itemToDelete);
    this._customersSubject.next(this._mapCustomers.map(c => c.customer));
    this._markersSubject.next(this._mapCustomers.map(c => c.marker));
  }

  async connectMarkers(data) {
    const {coordinated, problem} = await this.prepareRequestData(data);
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
      this._pathsSubject.next({solution: solution, paths: routes});
      this.setView(this._viewSubject.value + 1);
    }
  }

  private async prepareRequestData(data) {
    console.log(data);
    const coordinated = [this._depot.marker.getLatLng(), ...this._mapCustomers.map(c => c.marker.getLatLng())];
    console.log(coordinated);
    const distDur = await this._osrmService.getDistancesAndDurationsTable(coordinated).toPromise();
    const customers = this.prepareCustomers(coordinated, data);
    const problem = this.prepareProblem(data, coordinated, customers, distDur);
    return {coordinated, problem};
  }

  private prepareProblem(data, coordinated: LatLng[], customers: CustomerDto[], distDur: IDistDur) {
    return ProblemDto.fromJS({
      vehicles: data.problemInfo.vehicles,
      capacity: data.problemInfo.capacity,
      depot: DepotDto.fromJS({
        id: 0,
        x: Math.floor(coordinated[0].lng),
        y: Math.floor(coordinated[0].lat),
        dueDate: addHours(data.depotInfo.dueDate as Date, 1),
      }),
      customers: customers,
      distances: distDur.distances,
      durations: distDur.durations
    });
  }

  private prepareCustomers(coordinated: LatLng[], data) {
    const customers: CustomerDto[] = [];
    let time = 0;
    const today = new Date();
    today.setDate(today.getDate() - 1);
    console.log(data.customersInfoForm);

    for (let i = 1; i < coordinated.length; i++) {
      customers.push(CustomerDto.fromJS({
        id: i,
        x: Math.floor(coordinated[i].lng),
        y: Math.floor(coordinated[i].lat),
        demand: data.customersInfoForm.customersInfo[i - 1].demand,
        readyTime: addHours(data.customersInfoForm.customersInfo[i - 1].readyTime as Date, 1),
        dueDate: addHours(data.customersInfoForm.customersInfo[i - 1].dueDate as Date, 1),
        serviceTime: addHours(new Date(today.toDateString() + ' ' + data.customersInfoForm.customersInfo[i - 1].serviceTime), 1)
      }));
    }
    console.log(customers);
    return customers;
  }
}
