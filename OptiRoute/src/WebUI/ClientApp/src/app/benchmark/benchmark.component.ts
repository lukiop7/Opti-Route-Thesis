import { HttpClient, HttpEventType } from '@angular/common/http';
import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FileUploadControl, FileUploadValidators } from '@iplab/ngx-file-upload';
import { BenchmarksClient, FileParameter} from 'app/web-api-client';

@Component({
  selector: 'app-benchmark',
  templateUrl: './benchmark.component.html',
  styleUrls: ['./benchmark.component.scss']
})
export class BenchmarkComponent implements OnInit {
  public errors: string[] = [];
  public file: File = null;
  public fileName: string;
  @Output() public onUploadFinished = new EventEmitter();
  constructor(private _benchmarkClient: BenchmarksClient, private changeDetector: ChangeDetectorRef) { }
  ngOnInit() {
  }
  public uploadFile = async (files) => {
    this.errors = [];

    if (files.length === 0) {
      this.errors.push("File cannot be empty!")
      return;
    }

    let fileToUpload = <File>files[0];
    const extension = fileToUpload.name.split(".").pop();
    if (extension.toUpperCase() != "TXT") {
      this.errors.push("Txt file is required!")
      return;
    }
    this.file = fileToUpload;
    this.fileName = this.file.name;
    console.log(this.file);
     const formData = new FormData();
     formData.append('file', fileToUpload, fileToUpload.name);
     const file: FileParameter = {data: fileToUpload, fileName: fileToUpload.name};
    await this._benchmarkClient.getSolution(file)
    .toPromise()
    .then(result => {
      console.log(result);
    })
    .catch(
    error=>{
      let errors = JSON.parse(error.response);
      console.log(errors);
      if (errors && errors.errors.File) {
      this.errors = errors.errors.File;
      }
    });
  }

}
