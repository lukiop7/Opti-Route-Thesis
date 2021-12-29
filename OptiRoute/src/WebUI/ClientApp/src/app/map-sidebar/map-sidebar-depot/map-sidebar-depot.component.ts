import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { localIsoTime } from '../../../shared/utils/localIsoTime';
import { MapService } from '../../services/map.service';
import { Subscription } from 'rxjs';
import { Customer } from '../../../shared/models/customer';
import { getMinDate } from 'shared/utils/getMinDate';

@Component({
  selector: 'app-map-sidebar-depot',
  templateUrl: './map-sidebar-depot.component.html',
  styleUrls: ['./map-sidebar-depot.component.scss']
})
export class MapSidebarDepotComponent implements OnInit, OnDestroy {
  @Input('group')
  public depotInfoForm: FormGroup;
  @Output() continueClicked = new EventEmitter<void>();
  @Output() backClicked = new EventEmitter<void>();

  public minDate: string;
  public depot: Customer = null;
  private _depotSubscription: Subscription;

  constructor(private _mapService: MapService) {
    this.minDate = getMinDate();
  }
  ngOnDestroy(): void {
this._depotSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this._depotSubscription = this._mapService.getDepot().subscribe(value => {
      this.depot = value;
      console.log(this.depot );
    });
    if(!this.depotInfoForm.touched){
      console.log("patch");
      this.depotInfoForm.patchValue({
        dueDate: (localIsoTime().slice(0, 16)),
        readyTime: (localIsoTime().slice(0, 16))
      });
    } 
  }

  onClick() {
    this.continueClicked.emit();
  }

  onBackClick(){
    this.backClicked.emit();
  }


}

