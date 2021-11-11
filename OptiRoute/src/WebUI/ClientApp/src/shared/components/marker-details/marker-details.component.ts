import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {Customer} from '../../models/customer';
import {faTimes} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-marker-details',
  templateUrl: './marker-details.component.html',
  styleUrls: ['./marker-details.component.scss']
})
export class MarkerDetailsComponent implements OnInit {
  @Input()
  public customer: Customer;
  @Output()
  public closed = new EventEmitter<Customer>();

  public time: Date;
  public faTimes = faTimes;

  constructor() {
  }

  ngOnInit(): void {
    this.time = new Date();
  }

  closeOnClick() {
    this.closed.emit(this.customer);
  }
}
