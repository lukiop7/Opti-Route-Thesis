import {ChangeDetectorRef, Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {Customer} from '../../../shared/models/customer';
import {MapService} from '../../services/map.service';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-map-sidebar-customers',
  templateUrl: './map-sidebar-customers.component.html',
  styleUrls: ['./map-sidebar-customers.component.scss']
})
export class MapSidebarCustomersComponent implements OnInit, OnDestroy {
  @Input('group')
  public customersInfoForm: FormGroup;
  public customersInfo: FormArray;
  private _subscription: Subscription;
  customers: Customer[] = [];

  constructor(private changeDetector: ChangeDetectorRef, private _mapService: MapService, private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.customersInfo = <FormArray>this.customersInfoForm.controls['customersInfo'];
    this._subscription = this._mapService.getCustomers().subscribe((customers: Customer[]) => {
      this.customers = customers;
      if(this.customersInfo.length < this.customers.length){
        this.customersInfo.push(this.initCustomer());
      }
      this.changeDetector.detectChanges();
    });
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

  onClick() {
    //this._mapService.connectMarkers();
  }

  removeCustomer(customer: Customer) {
    const index = this.customers.indexOf(customer);
    this.customersInfo.removeAt(index);
    this._mapService.removeCustomer(customer);
  }

  initCustomer(): FormGroup {
    return this.fb.group({
      demand: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      readyTime: [null, Validators.required],
      dueDate: [null, Validators.required],
      serviceTime: [null, Validators.required],
    });
  }
}
