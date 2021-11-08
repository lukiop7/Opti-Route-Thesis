import {ChangeDetectorRef, Component, EventEmitter, Input, NgZone, OnDestroy, OnInit, Output} from '@angular/core';
import {
  icon,
  LatLng,
  latLng,
  Layer,
  LeafletMouseEvent,
  marker,
  polyline,
  tileLayer,
  Map as lMap,
  popup,
  Polyline,
  Marker,
  Polygon
} from 'leaflet';
import {MapService} from '../services/map.service';
import {Subscription} from 'rxjs';

import 'leaflet';
import 'leaflet-routing-machine';
import {OsrmService} from '../services/osrm.service';

declare let L;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, OnDestroy {
  private _markersSubscription: Subscription;
  private _pathsSubscription: Subscription;
  public showPath = false;
  allLayers: Layer[] = [];
  pathLayers: Layer[] = [];
  options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {maxZoom: 18, attribution: '...'})
    ],
    zoom: 5,
    center: latLng(50.276093992810296, 18.93446445465088)
  };
  map: lMap;
  counter = 0;
  path: any;
  colors: string[] = ['blue', 'red', 'green'];
  colorsCounter = 0;

  constructor(private changeDetector: ChangeDetectorRef, private _mapService: MapService, private _osrmService: OsrmService) {
  }

  ngOnInit(): void {
    this._markersSubscription = this._mapService.getMarkers().subscribe((markers: Marker[]) => {
      this.allLayers = markers;
      this.changeDetector.detectChanges();
    });
    this._pathsSubscription = this._mapService.getPaths().subscribe((paths: LatLng[][]) => {
      this.colorsCounter = 0;

      paths.forEach(path => {
        const routeControl = L.Routing.control({
          waypoints: path,
          routeWhileDragging: false,
          lineOptions: {
            styles: [{color: this.colors[this.colorsCounter++], opacity: 1, weight: 5}]
          }
        }).addTo(this.map);

      });

      // // this.pathLayers.push(paths);
      // routeControl.on('routesfound', function(e) {
      //   const routes = e.routes;
      //   const summary = routes[0].summary;
      // });
    });
  }

  ngOnDestroy(): void {
    this._markersSubscription.unsubscribe();
    this._pathsSubscription.unsubscribe();
  }

  onMapReady(map: L.Map) {
    this.map = map;
    map.on('click', (e: LeafletMouseEvent) => {
      this.addMarker(e.latlng);
      this.changeDetector.detectChanges();
    });
  }

  addMarker(latlng: LatLng) {
    const newMarker = marker(
      [latlng.lat, latlng.lng],
      {
        icon: icon({
          iconSize: [25, 41],
          iconAnchor: [13, 41],
          iconUrl: 'leaflet/marker-icon.png',
          shadowUrl: 'leaflet/marker-shadow.png'
        })
      }
    );
    this._mapService.addMarker(newMarker);
  }

  // removeMarker() {
  //   const marker = this.markers[this.markers.length - 1];
  //   this.customersMap.delete(marker);
  //   removeItem(this.allLayers, marker);
  //   this.counter++;
  //   this.customersValues = [...this.customersMap.values()];
  // }

  connectMarkers() {
    // const coordinated = this.markers.map(c => (c as Marker).getLatLng());
    // const path = new Polygon(coordinated, {color: 'red', fillOpacity: 0});
    // console.log(path.getLatLngs());
    // console.log(path.getLatLngs());
    // this.paths.push(path);
    // this.allLayers = [...this.markers, ...this.paths];
  }

}
