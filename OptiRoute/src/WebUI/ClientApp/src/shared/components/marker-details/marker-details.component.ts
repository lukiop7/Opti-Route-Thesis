import {Component, Input, OnInit, Output, EventEmitter, ChangeDetectionStrategy, OnChanges, SimpleChanges, ChangeDetectorRef, OnDestroy} from '@angular/core';
import {Customer} from '../../models/customer';
import {faTimes} from '@fortawesome/free-solid-svg-icons';
import {FormGroup} from '@angular/forms';
import {localIsoTime} from '../../utils/localIsoTime';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-marker-details',
  templateUrl: './marker-details.component.html',
  styleUrls: ['./marker-details.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MarkerDetailsComponent implements OnInit, OnDestroy  {
  @Input()
  public customer: Customer;
  @Input()
  public id: number;
  @Input('group')
  public customerInfoForm: FormGroup;
  @Input('depot')
  public depotForm: FormGroup;
  @Output()
  public closed = new EventEmitter<Customer>();

  public faTimes = faTimes;

  private changeSubscription: Subscription;

  constructor(private _changeDetector:ChangeDetectorRef) {
  }
  ngOnDestroy(): void {
    this.changeSubscription.unsubscribe();
  }

  ngOnInit(): void {
    const currentDate = localIsoTime();
    if(!this.customerInfoForm.touched){
    this.customerInfoForm.patchValue({
      dueDate: (this.depotForm.controls.dueDate.value.slice(0,16)),
      readyTime: (this.depotForm.controls.readyTime.value.slice(0,16))
    });

    this.changeSubscription = this.customerInfoForm.valueChanges.subscribe(changes=>{
       this._changeDetector.detectChanges();
       console.log(changes);
      });
  }
  }

  closeOnClick() {
    this.closed.emit(this.customer);
  }

}
