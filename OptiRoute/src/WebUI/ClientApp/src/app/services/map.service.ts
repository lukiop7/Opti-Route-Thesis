import {Injectable} from '@angular/core';
import {MapCustomer} from '../../shared/models/mapCustomer';
import {Customer} from '../../shared/models/customer';
import {LatLng, Marker, Polygon} from 'leaflet';
import {removeItem} from '../../shared/utils/removeItem';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  private _mapCustomers: MapCustomer[] = [];
  private _markersSubject = new Subject<Marker[]>();
  private _customersSubject = new Subject<Customer[]>();
  private _pathsSubject = new Subject<LatLng[]>();

  constructor() {
  }

  getCustomers(): Observable<Customer[]> {
    return this._customersSubject.asObservable();
  }

  getMarkers(): Observable<Marker[]> {
    return this._markersSubject.asObservable();
  }

  getPaths(): Observable<LatLng[]> {
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

  connectMarkers() {
    const coordinated = this._mapCustomers.map(c => c.marker.getLatLng());
    //const path = new Polygon(coordinated, {color: 'red', fillOpacity: 0});
    this._pathsSubject.next(coordinated);
  }
}
