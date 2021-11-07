import {Customer} from './customer';
import {Marker} from 'leaflet';

export interface MapCustomer {
  customer: Customer;
  marker: Marker;
}
