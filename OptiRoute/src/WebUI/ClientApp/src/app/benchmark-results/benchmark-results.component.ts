import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BenchmarkResultDetailsDto, BenchmarkResultDto, BenchmarksClient } from 'app/web-api-client';
import { VisualizerComponent } from 'shared/components/visualizer/visualizer.component';

@Component({
  selector: 'app-benchmark-results',
  templateUrl: './benchmark-results.component.html',
  styleUrls: ['./benchmark-results.component.scss']
})
export class BenchmarkResultsComponent implements OnInit, AfterViewInit {
  public displayedColumns = ["name", "distance", "vehicles", "bestDistance", "bestVehicles", "actions"];
  public dataSource = new MatTableDataSource<BenchmarkResultDto>();
  selectedBenchmark: BenchmarkResultDetailsDto;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(private client: BenchmarksClient, private visualizeDialog: MatDialog) {

  }

  ngOnInit() {
    this.getBenchmarkResults();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  private getBenchmarkResults() {
    this.client.getBenchmarkResultsAll()
      .subscribe((result) => {
        this.dataSource.data = result;
      })
  }

  private getBenchmarkResultById(id: number) {
   return this.client.getBenchmarkResults(id).toPromise();
  }

  async onVisualizeClicked(id: number) {
    this.selectedBenchmark = await this.getBenchmarkResultById(id);
    this.visualizeDialogOpen();
  }

  private visualizeDialogOpen() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;
    dialogConfig.height = "80vh";
    dialogConfig.width = "80vw";

    dialogConfig.data = {
      solution: this.selectedBenchmark.solutionDto
    }

    this.visualizeDialog.open(VisualizerComponent, dialogConfig);
  }
}
