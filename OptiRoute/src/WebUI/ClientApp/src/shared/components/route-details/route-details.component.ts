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

  public isExpanded: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
    console.log(this.route);
  }

  @HostListener('mouseenter') mouseover(event: Event) {
    this.routeMouseOver.emit(this.route);
    this.isExpanded = true;
  }
  @HostListener('mouseleave') mouseout(event: Event) {
    this.routeMouseOver.emit(this.route);
    this.isExpanded = false;
  }
}
