import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'distanceFormatted'
})
export class DistanceFormattedPipe implements PipeTransform {

    transform(value: number): string {
      const meters: number = Math.floor(value % 1000);
       const kilometes: number = Math.floor(value / 1000);
       return `${kilometes}km ${meters}m`;
    }
}