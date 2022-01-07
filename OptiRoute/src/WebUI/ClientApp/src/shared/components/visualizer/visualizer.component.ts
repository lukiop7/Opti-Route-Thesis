import { AfterViewInit, Component, ElementRef, HostListener, Inject, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RouteDto, SolutionDto } from 'app/web-api-client';
import { randomColor } from 'shared/utils/randomColor';

@Component({
  selector: 'app-visualizer',
  templateUrl: './visualizer.component.html',
  styleUrls: ['./visualizer.component.scss'],

})
export class VisualizerComponent implements OnInit, AfterViewInit {
  @ViewChild("visualizationCanvas") canvas: ElementRef;
  public canvasContext: CanvasRenderingContext2D;
  public solution: SolutionDto
  private minX: number;
  private maxX: number;
  private minY: number;
  private maxY: number;
  private scaleX: number;
  private scaleY: number;


  constructor(private dialogRef: MatDialogRef<VisualizerComponent>, @Inject(MAT_DIALOG_DATA) data) {
    this.solution = data.solution;
  }


  ngAfterViewInit(): void {
    this.calculateMinMaxValues();
    this.canvasContext = this.canvas.nativeElement.getContext("2d");

    this.drawSolution();
  }

  ngOnInit() {

  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.canvasContext.clearRect(0, 0, this.canvas.nativeElement.width, this.canvas.nativeElement.height);

    this.drawSolution();
  }

  private drawSolution() {
    this.setUpCanvas();
    this.calculateScale();
    this.canvasContext.translate(this.canvas.nativeElement.width * 0.05, this.canvas.nativeElement.height * 0.05);
    this.drawRoute();
  }

  private drawRoute() {
    for (let i = 0; i < this.solution.routes.length; i++) {
      this.canvasContext.fillStyle = randomColor(i);
      this.canvasContext.strokeStyle = this.canvasContext.fillStyle;

      this.drawCustomers(this.solution.routes[i]);
      this.connectDepot(this.solution.routes[i]);
    }
  }

  private drawCustomers(route: RouteDto) {
    for (let i = 0; i < route.customers.length; i++) {
      this.canvasContext.beginPath();
      this.canvasContext.arc(this.scaleCoordinateX(route.customers[i].x), this.scaleCoordinateY(route.customers[i].y), 3, 0, 2 * Math.PI);
      this.canvasContext.fill();

      if (i === 0)
        continue;

      this.drawLine(this.scaleCoordinateX(route.customers[i - 1].x), this.scaleCoordinateY(route.customers[i - 1].y),
        this.scaleCoordinateX(route.customers[i].x), this.scaleCoordinateY(route.customers[i].y));
    }
  }

  private connectDepot(route: RouteDto) {
    this.drawLine(this.scaleCoordinateX(this.solution.depot.x), this.scaleCoordinateY(this.solution.depot.y),
      this.scaleCoordinateX(route.customers[0].x), this.scaleCoordinateY(route.customers[0].y));
    this.drawLine(this.scaleCoordinateX(this.solution.depot.x), this.scaleCoordinateY(this.solution.depot.y),
      this.scaleCoordinateX(route.customers[route.customers.length - 1].x), this.scaleCoordinateY(route.customers[route.customers.length - 1].y));
  }

  private drawLine(srcX, srcY, destX, destY) {
    this.canvasContext.beginPath();
    this.canvasContext.moveTo(srcX, srcY);
    this.canvasContext.lineTo(destX, destY);
    this.canvasContext.stroke();
  }

  private setUpCanvas() {
    this.canvas.nativeElement.width = this.canvas.nativeElement.parentElement.clientWidth;
    this.canvas.nativeElement.height = this.canvas.nativeElement.parentElement.clientHeight;
  }

  private calculateMinMaxValues() {
    this.minX = Math.min.apply(Math, [...this.solution.routes.map(function (o) { return Math.min(...o.customers.map(function (c) { return c.x })); }),
    this.solution.depot.x]);
    this.maxX = Math.max.apply(Math, [...this.solution.routes.map(function (o) { return Math.max(...o.customers.map(function (c) { return c.x })); }),
      this.solution.depot.x]);
    this.minY = Math.min.apply(Math, [...this.solution.routes.map(function (o) { return Math.min(...o.customers.map(function (c) { return c.y })); }),
      this.solution.depot.y]);
    this.maxY = Math.max.apply(Math, [...this.solution.routes.map(function (o) { return Math.max(...o.customers.map(function (c) { return c.y })); }),
      this.solution.depot.y]);
  }

  private calculateScale() {
    this.scaleX = (this.canvas.nativeElement.width * 0.9) / (this.maxX+this.minX);
    this.scaleY = (this.canvas.nativeElement.height * 0.9) / (this.maxY+this.minY);
  }

  private scaleCoordinateX(x) {
    return (x) * this.scaleX;
  }

  private scaleCoordinateY(y) {
    return (y) * this.scaleY;
  }

}
