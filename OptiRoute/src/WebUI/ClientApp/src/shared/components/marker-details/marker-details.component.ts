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
    this.customerInfoForm.patchValue({
      dueDate: (currentDate.slice(0, 16)),
      readyTime: (currentDate.slice(0, 16)),
      serviceTime:  (currentDate.slice(11, 16)),
    });
  }

  closeOnClick() {
    this.closed.emit(this.customer);
  }

}
