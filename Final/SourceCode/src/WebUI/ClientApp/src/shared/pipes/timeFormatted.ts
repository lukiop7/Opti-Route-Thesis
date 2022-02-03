import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'timeFormatted'
})
export class TimeFormattedPipe implements PipeTransform {

    transform(value: number): string {
      const hours: number = Math.floor(value / 3600);
      let seconds: number = Math.floor(value % 3600);
      const minutes: number = Math.floor(seconds /60);
      seconds = Math.floor(seconds % 60);
       return `${hours}h ${minutes}m ${seconds}s`;
    }
}