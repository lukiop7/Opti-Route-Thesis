import {ChangeDetectorRef, Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {Customer} from '../../../shared/models/customer';
import {MapService} from '../../services/map.service';

@Component({
  selector: 'app-map-sidebar-customers',
  templateUrl: './map-sidebar-customers.component.html',
  styleUrls: ['./map-sidebar-customers.component.scss']
})
export class MapSidebarCustomersComponent implements OnInit, OnDestroy {
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

  onClick() {
    this._mapService.connectMarkers();
  }

  removeCustomer(customer: Customer) {
    this._mapService.removeCustomer(customer);
  }
}
