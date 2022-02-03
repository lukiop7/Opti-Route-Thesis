import {Component, OnDestroy, OnInit} from '@angular/core';
import {animate, style, transition, trigger} from '@angular/animations';
import { MapService } from 'app/services/map.service';

@Component({
  selector: 'app-map-layout',
  templateUrl: './map-layout.component.html',
  styleUrls: ['./map-layout.component.scss'],
  providers: [MapService]
})
export class MapLayoutComponent implements OnInit, OnDestroy {



  constructor() {
  }
  ngOnDestroy(): void {
  }

  ngOnInit(): void {
  }

}
