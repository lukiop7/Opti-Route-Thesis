import { ChangeDetectorRef, Component, EventEmitter, Input, NgZone, OnDestroy, OnInit, Output } from '@angular/core';
import { control, Polyline } from 'leaflet';

import { MapService } from '../services/map.service';
import { Subscription } from 'rxjs';

declare let L;
import 'leaflet';
import 'leaflet-routing-machine';

import { OsrmService } from '../services/osrm.service';
import zoom = control.zoom;
import { VrptwSolutionResponse } from '../../shared/models/vrptwSolutionResponse';
import { randomColor } from 'shared/utils/randomColor';
import { delay } from 'shared/utils/delay';

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
  private _selectedPathIndexSubscription: Subscription;
  private _viewCounter: number;
  private _selectedPathIndex: number;
  private _selectedPolyLine: any;
  public showPath = false;
  private index:number = 400;
  customersLayer: L.LayerGroup;
  depotLayer: L.LayerGroup;
  pathsLayer: any[] = [];
  panes: any[] = [];
  options = {
    layers: [
      L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '...' })
    ],
    zoom: 5,
    center: L.latLng(50.276093992810296, 18.93446445465088),
    zoomControl: false
  };

  map: L.Map;
  counter = 0;
  path: any;

  constructor(private changeDetector: ChangeDetectorRef, private _mapService: MapService, private _osrmService: OsrmService) {
  }

  ngOnInit(): void {
    this._markersSubscription = this._mapService.getMarkers().subscribe((markers: L.Marker[]) => {
      this.customersLayer.clearLayers();
      markers.forEach(marker => {
        this.customersLayer.addLayer(marker);
      });
      this.refreshToolTips();
      this.changeDetector.detectChanges();
    });

    this._depotMarkerSubscription = this._mapService.getDepotMarker().subscribe((depot: L.Marker) => {
      this.depotLayer.clearLayers();
      this.depotLayer.addLayer(depot);
      this.changeDetector.detectChanges();
    });

    this._pathsSubscription = this._mapService.getPaths().subscribe((result: VrptwSolutionResponse) => {
      this.handlePaths(result);
    });

    this._viewSubscription = this._mapService.getView().subscribe(value => {
      this._viewCounter = value;
    });

    this._selectedPathIndexSubscription = this._mapService.getSelectedPath().subscribe(value => {
      this._selectedPathIndex = value;
      let max = 0;
      let index = 0;
      this.panes.forEach(function (v, k) {
        if (max < +v.style.zIndex) {
          max = +v.style.zIndex;
          index = k;
        }
      });
      const tmp = this.panes[this._selectedPathIndex].style.zIndex;
      this.panes[this._selectedPathIndex].style.zIndex = max;
      this.panes[index].style.zIndex = tmp;

      if (this._selectedPolyLine != null) {
        this.map.removeControl(this._selectedPolyLine);
      }
      // Background continuous black stroke, wider than dashed line.
      this._selectedPolyLine = L.polyline(this.pathsLayer[this._selectedPathIndex]._selectedRoute.coordinates, {
        weight: 15,
        lineCap: 'square', // Optional, just to avoid round borders.
        color: '#757373'
      }).addTo(this.map);
    });
  }

  private async handlePaths(result: VrptwSolutionResponse) {
    if (this._selectedPolyLine != null) {
      this.map.removeControl(this._selectedPolyLine);
    }
    console.log(`pane ${this.panes.length}`);
    this.pathsLayer.forEach(path => {
      this.map.removeControl(path);
    });
    this.pathsLayer = [];
    console.log(`paths ${this.pathsLayer}`);
    //  this.pathsLayer.clearLayers();
    for (let i = 0; i < result.paths.length; i++) {
      if (i!= 0 && i % 10 === 0)
        await delay(4000);

      const paneName = `pane${i}`;
      if(i >  this.panes.length - 1){
        const pane = this.map.createPane(paneName);
        pane.style.zIndex = this.index.toString();
        this.index += 1;
      this.panes.push(pane);
      }
      const routeControl = L.Routing.control({
        createMarker: function () {
          return null;
        },
        waypoints: result.paths[i],
        routeWhileDragging: false,
        show: false,
        fitSelectedRoutes: false,
        lineOptions: {
          addWaypoints: false,
          styles: [{ pane: paneName, color: randomColor(i), opacity: 1, weight: 5 }]
        }
      }).addTo(this.map);
      routeControl.hide();
      this.pathsLayer.push(routeControl);
    }
    this.changeDetector.detectChanges();
  }

  ngOnDestroy(): void {
    this._markersSubscription.unsubscribe();
    this._pathsSubscription.unsubscribe();
    this._depotMarkerSubscription.unsubscribe();
    this._viewSubscription.unsubscribe();
  }

  onMapReady(map: L.Map) {
    this.map = map;
    map.on('click', (e: L.LeafletMouseEvent) => {
      this.addMarker(e.latlng);
      this.changeDetector.detectChanges();
    });
    this.map.addControl(zoom({
      position: 'bottomright'
    }));
    this.customersLayer = new L.LayerGroup();
    this.customersLayer.addTo(this.map);
    this.depotLayer = new L.LayerGroup();
    this.depotLayer.addTo(this.map);
    // this.pathsLayer = new L.LayerGroup();
    // this.pathsLayer.addTo(this.map);
  }

  addMarker(latlng: L.LatLng) {
    if (this.depotLayer.getLayers().length > 0 && this._viewCounter === 3) {
      const newMarker = L.marker(
        [latlng.lat, latlng.lng],
        {
          icon:
            L.icon({
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
      const newMarker = L.marker(
        [latlng.lat, latlng.lng],
        {
          icon:
            L.icon({
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
}
