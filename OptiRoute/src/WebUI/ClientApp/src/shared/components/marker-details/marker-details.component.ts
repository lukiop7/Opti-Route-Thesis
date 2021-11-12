import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {Customer} from '../../models/customer';
import {faTimes} from '@fortawesome/free-solid-svg-icons';
import {FormGroup} from '@angular/forms';

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

  public dueDate: Date;
  public readyTime: Date;
  public serviceTime: Date;
  public demand = 0;
  public faTimes = faTimes;

  constructor() {
  }

  ngOnInit(): void {
    this.dueDate = new Date();
    this.readyTime = new Date();
    this.serviceTime = new Date();
  }

  closeOnClick() {
    this.closed.emit(this.customer);
  }
}
