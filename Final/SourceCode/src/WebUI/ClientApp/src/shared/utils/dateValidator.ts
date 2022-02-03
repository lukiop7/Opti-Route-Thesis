import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const dateValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const start = control.get('readyTime');
    const end = control.get('dueDate');
    return start.value !== null && end.value !== null && start.value < end.value 
    ? null :{ dateValid:true };
      }