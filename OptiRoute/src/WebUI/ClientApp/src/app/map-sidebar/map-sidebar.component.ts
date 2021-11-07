import {ChangeDetectorRef, Component, OnDestroy, OnInit} from '@angular/core';
import {MapCustomer} from '../../shared/models/mapCustomer';
import {Customer} from '../../shared/models/customer';
import {Observable, Subscription} from 'rxjs';
import {Marker} from 'leaflet';
import {MapService} from '../services/map.service';

@Component({
  selector: 'app-map-sidebar',
  templateUrl: './map-sidebar.component.html',
  styleUrls: ['./map-sidebar.component.scss']
})
export class MapSidebarComponent implements OnInit, OnDestroy {
  private _subscription: Subscription;
  customers: Customer[] = [];

  constructor(private changeDetector: ChangeDetectorRef, private _mapService: MapService) {
  }

  ngOnInit(): void {
    this._subscription = this._mapService.getCustomers().subscribe((customers: Customer[]) => {
      this.customers = customers;
      this.changeDetector.detectChanges();
    });
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

  onClick(){
    this._mapService.connectMarkers();
  }

}
