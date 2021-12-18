import {SolutionDto} from '../../app/web-api-client';
import {LatLng} from 'leaflet';

export interface VrptwSolutionResponse {
solution: SolutionDto;
  paths: LatLng[][];
}
