import {ChangeDetectorRef, Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {Subscription} from 'rxjs';
import {MapService} from '../../services/map.service';
import {VrptwSolutionResponse} from '../../../shared/models/vrptwSolutionResponse';
import {RouteDto} from '../../web-api-client';

@Component({
  selector: 'app-map-sidebar-solution',
  templateUrl: './map-sidebar-solution.component.html',
  styleUrls: ['./map-sidebar-solution.component.scss']
})
export class MapSidebarSolutionComponent implements OnInit, OnDestroy {
  @Output() backClicked = new EventEmitter<void>();
  
  private _solutionSubscription: Subscription;
  solution: VrptwSolutionResponse;

  constructor(private changeDetector: ChangeDetectorRef, private _mapService: MapService) {
  }

  ngOnInit(): void {
    this._solutionSubscription = this._mapService.getPaths().subscribe((result: VrptwSolutionResponse) => {
      this.solution = result;
    });
  }

  ngOnDestroy(): void {
    this._solutionSubscription.unsubscribe();
  }

  selectedRouteChanged(route: RouteDto) {
    this._mapService.setSelectedPath(this.solution.solution.routes.indexOf(route));
  }

  onBackClick(){
    this.backClicked.emit();
  }
}
