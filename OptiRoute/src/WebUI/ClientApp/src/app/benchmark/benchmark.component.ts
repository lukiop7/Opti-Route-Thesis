import { HttpClient, HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';
import { BenchmarksClient, FileParameter, SolutionDto } from 'app/web-api-client';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-benchmark',
  templateUrl: './benchmark.component.html',
  styleUrls: ['./benchmark.component.scss']
})
export class BenchmarkComponent implements OnInit {
  public errors: string[] = [];
  public file: File = null;
  public fileName: string;
  public solution: SolutionDto = null;
  @Output() public onUploadFinished = new EventEmitter();
  @ViewChild("fileInput") input : ElementRef;

  constructor(private _benchmarkClient: BenchmarksClient, private changeDetector: ChangeDetectorRef, private toastr: ToastrService) {
  }

  ngOnInit() {
  }


  public fileSelected = async (files) => {
    this.solution = null; 
    if (!this.validateFile(files))
      return;

    const fileToUpload = <File>files[0];
    this.file = fileToUpload;
    this.fileName = this.file.name;
  }

  public async uploadFile() {
    await this.uploadFileApi(this.file);
  }

  public fileButtonClick(){
    this.input.nativeElement.value = null;
    this.input.nativeElement.click();
  }

  private validateFile(files) {
    this.errors = [];

    if (files.length === 0) {
      this.errors.push("File cannot be empty!")
      this.file = null;
      this.fileName = null;
      return false;
    }

    const fileToUpload = <File>files[0];
    const extension = fileToUpload.name.split(".").pop();
    if (extension.toUpperCase() != "TXT") {
      this.errors.push("Txt file is required!")
      return false;
    }

    return true;
  }

  private async uploadFileApi(fileToUpload) {
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    const file: FileParameter = { data: fileToUpload, fileName: fileToUpload.name };
    await this._benchmarkClient.getSolution(file)
      .toPromise()
      .then(result => {
        this.solution = result;
        if(this.solution.feasible){
          this.toastr.success("Your problem is feasible!", "Success!");
        }
        else{
          this.toastr.warning("Your problem cannot be solved", "Problem is not feasible");
        }
      })
      .catch(
        error => {
          this.toastr.error("Something went wrong. Try again!", "Oops!");
          let errors = JSON.parse(error.response);
          if (errors && errors.errors.File) {
            this.errors = errors.errors.File;
          }
        });
  }

}
