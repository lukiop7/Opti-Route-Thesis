import {Component, EventEmitter, HostListener, Input, OnInit, Output} from '@angular/core';
import {Customer} from '../../models/customer';
import {RouteDto} from '../../../app/web-api-client';

@Component({
  selector: 'app-route-details',
  templateUrl: './route-details.component.html',
  styleUrls: ['./route-details.component.scss']
})
export class RouteDetailsComponent implements OnInit {
  @Input()
  public route: RouteDto;
  @Output()
  public routeMouseOver = new EventEmitter<RouteDto>();

  constructor() {
  }

  ngOnInit(): void {
  }

  @HostListener('mouseenter') mouseover(event: Event) {
    this.routeMouseOver.emit(this.route);
  }
}
