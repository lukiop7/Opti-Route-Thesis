import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {Customer} from '../../models/customer';
import {faTimes} from '@fortawesome/free-solid-svg-icons';
import {FormGroup} from '@angular/forms';
import {localIsoTime} from '../../utils/localIsoTime';

@Component({
  selector: 'app-marker-details',
  templateUrl: './marker-details.component.html',
  styleUrls: ['./marker-details.component.scss']
})
export class MarkerDetailsComponent implements OnInit {
  @Input()
  public customer: Customer;
  @Input('group')
  public customerInfoForm: FormGroup;
  @Output()
  public closed = new EventEmitter<Customer>();

  public faTimes = faTimes;

  constructor() {
  }

  ngOnInit(): void {
    const currentDate = localIsoTime();
    if(!this.customerInfoForm.touched){
    this.customerInfoForm.patchValue({
      dueDate: (currentDate.slice(0, 16).replace("13","23")),
      readyTime: (currentDate.slice(0, 16)),
      serviceTime: '00:01',
      demand: 1
    });
  }
  }

  closeOnClick() {
    this.closed.emit(this.customer);
  }

}
