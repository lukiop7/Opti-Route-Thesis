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
  Zoom, control, LayerGroup
} from 'leaflet';
import {MapService} from '../services/map.service';
import {Subscription} from 'rxjs';

import 'leaflet';
import 'leaflet-routing-machine';
import {OsrmService} from '../services/osrm.service';
import zoom = control.zoom;

declare let L;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, OnDestroy {
  private _markersSubscription: Subscription;
  private _pathsSubscription: Subscription;
  private _depotMarkerSubscription: Subscription;
  private _viewSubscription: Subscription;
  private _viewCounter: number;
  public showPath = false;
  customersLayer: LayerGroup;
  depotLayer: LayerGroup;
  pathsLayer: LayerGroup;
  options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {maxZoom: 18, attribution: '...'})
    ],
    zoom: 5,
    center: latLng(50.276093992810296, 18.93446445465088),
    zoomControl: false
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
      this.customersLayer.clearLayers();
      markers.forEach(marker => {
        this.customersLayer.addLayer(marker);
      });
      this.refreshToolTips();
      this.changeDetector.detectChanges();
    });

    this._depotMarkerSubscription = this._mapService.getDepot().subscribe((depot: Marker) => {
      this.depotLayer.clearLayers();
      this.depotLayer.addLayer(depot);
      this.changeDetector.detectChanges();
    });

    this._pathsSubscription = this._mapService.getPaths().subscribe((paths: LatLng[][]) => {
      this.pathsLayer.clearLayers();
      this.colorsCounter = 0;
      paths.forEach(path => {
        const routeControl = L.Routing.control({
          waypoints: path,
          routeWhileDragging: false,
          lineOptions: {
            styles: [{color: this.colors[this.colorsCounter++], opacity: 1, weight: 5}]
          }
        });
        this.pathsLayer.addLayer(routeControl);
        this.changeDetector.detectChanges();
      });
    });

    this._viewSubscription = this._mapService.getView().subscribe(value => {
      this._viewCounter = value;
    });
  }

  ngOnDestroy(): void {
    this._markersSubscription.unsubscribe();
    this._pathsSubscription.unsubscribe();
    this._depotMarkerSubscription.unsubscribe();
    this._viewSubscription.unsubscribe();
  }

  onMapReady(map: L.Map) {
    this.map = map;
    map.on('click', (e: LeafletMouseEvent) => {
      this.addMarker(e.latlng);
      this.changeDetector.detectChanges();
    });
    this.map.addControl(zoom({
      position: 'bottomright'
    }));
    this.customersLayer = new LayerGroup<any>();
    this.customersLayer.addTo(this.map);
    this.depotLayer = new LayerGroup<any>();
    this.depotLayer.addTo(this.map);
    this.pathsLayer = new LayerGroup<any>();
    this.pathsLayer.addTo(this.map);
  }

  addMarker(latlng: LatLng) {
    if (this.depotLayer.getLayers().length > 0 && this._viewCounter === 3) {
      const newMarker = marker(
        [latlng.lat, latlng.lng],
        {
          icon:
            icon({
              iconSize: [25, 41],
              iconAnchor: [13, 41],
              iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-blue.png',
              shadowUrl: 'leaflet/marker-shadow.png'
            })
        });
      newMarker.bindTooltip(`${this.customersLayer.getLayers().length + 1}`,
        {
          permanent: true,
          direction: 'center'
        }
      );
      this._mapService.addMarker(newMarker);
    } else if (this._viewCounter === 2) {
      const newMarker = marker(
        [latlng.lat, latlng.lng],
        {
          icon:
            icon({
              iconSize: [25, 41],
              iconAnchor: [13, 41],
              iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-red.png',
              shadowUrl: 'leaflet/marker-shadow.png'
            })
        });
      newMarker.bindTooltip('depot',
        {
          permanent: true,
          direction: 'center'
        }
      );
      this._mapService.addDepot(newMarker);

    }
  }

  refreshToolTips() {
    for (let i = 0; i < this.customersLayer.getLayers().length; i++) {
      const marker = this.customersLayer.getLayers()[i];
      marker.getTooltip().setContent(`${i + 1}`);
    }
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
