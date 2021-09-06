import { FormGroupControls } from './../../../shared/models/form-group-controls.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { TestService } from 'src/app/shared/services/test/test.service';

@Component({
  selector: 'app-test-crud',
  templateUrl: './test-crud.component.html',
  styleUrls: ['./test-crud.component.scss']
})
export class TestCRUDComponent implements OnInit {
  testForm: FormGroup;
  showPassword = false;
  loading = false;

  name = new FormControl('', [
    Validators.required,
  ]);

  age = new FormControl('', [Validators.required]);


  get form(): FormGroupControls {
    return this.testForm.controls;
  }

  constructor(
    private fb: FormBuilder,
    private testService: TestService,
    private router: Router,) {
      this.testForm = this.fb.group({
        name: this.name,
        age: this.age,
      });
     }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.loading = true;
     this.testService
       .createUserAccount(this.form.name.value, this.form.age.value)
       .subscribe(
        (data: any) => {
          this.loading = false;
            this.router.navigateByUrl("test/module");
        },
     );
  }
}
