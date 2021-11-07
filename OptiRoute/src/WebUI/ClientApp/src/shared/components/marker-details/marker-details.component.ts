import {Component, Input, OnInit} from '@angular/core';
import {Customer} from '../../models/customer';

@Component({
  selector: 'app-marker-details',
  templateUrl: './marker-details.component.html',
  styleUrls: ['./marker-details.component.scss']
})
export class MarkerDetailsComponent implements OnInit {
@Input()
  public customer: Customer;

  constructor() { }

  ngOnInit(): void {
  }

}
