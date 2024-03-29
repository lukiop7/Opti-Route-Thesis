import {ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {MapCustomer} from '../../shared/models/mapCustomer';
import {Customer} from '../../shared/models/customer';
import {Observable, Subscription} from 'rxjs';
import {Marker} from 'leaflet';
import {MapService} from '../services/map.service';
import {animate, style, transition, trigger} from '@angular/animations';
import {FormBuilder, FormGroup, NgForm, Validators} from '@angular/forms';
import {minLengthArray} from '../../shared/utils/minLengthArray';
import { dateValidator } from 'shared/utils/dateValidator';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-map-sidebar',
  templateUrl: './map-sidebar.component.html',
  styleUrls: ['./map-sidebar.component.scss'],
  animations: [
    trigger('fadeSlideInOut', [
      transition(':enter', [
        style({opacity: 0, transform: 'translateX(10px)'}),
        animate('500ms', style({opacity: 1, transform: 'translateX(0)'})),
      ]),
      transition(':leave', [
        animate('500ms', style({opacity: 0, transform: 'translateX(10px)'})),
      ]),
    ]),
  ],
  encapsulation: ViewEncapsulation.None
})
export class MapSidebarComponent implements OnInit, OnDestroy {
  @ViewChild('dataForm') dataFormElement: NgForm;
  
  public firstShow = false;
  public vrptwForm: FormGroup;
  public viewCounter = 0;
  private _viewSubscription: Subscription;

  constructor(private toastr: ToastrService,private changeDetector: ChangeDetectorRef, private _mapService: MapService, private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.initializeForm();
    this._viewSubscription = this._mapService.getView().subscribe(value =>{
      this.viewCounter = value;
    });
  }

  ngOnDestroy(): void {
    this._viewSubscription.unsubscribe();
  }

  viewShown() {
    this._mapService.setView(this.viewCounter + 1);
  }

  previousView() {
    this._mapService.setView(this.viewCounter - 1);
  }

  initializeForm() {
    this.vrptwForm = this.fb.group({
      depotInfo: this.fb.group({
        readyTime: [null, Validators.required],
        dueDate: [null, Validators.required],
        vehicles: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
        capacity: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      },
      {validators: dateValidator}),
      customersInfoForm: this.fb.group({
          customersInfo: this.fb.array([],minLengthArray(1))
        }
      )
    });
  }

  solveClicked(){
   this.submitted();
  }

  submitted() {
    this._mapService.connectMarkers(this.vrptwForm.value);
  }
}
