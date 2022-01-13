import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const depotDateValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    if (!control.root || !control.parent) {
        return null;
      }

    const start = control.get('readyTime');
    const end = control.get('dueDate');
    const startDepot = control.parent.parent.parent.get('depotInfo').get('readyTime');
    const endDepot = control.parent.parent.parent.get('depotInfo').get('dueDate');

    return start.value !== null && end.value && startDepot.value !== null && endDepot.value !== null
    && start.value < end && startDepot.value < start.value && endDepot.value > start.value
    && startDepot.value < end.value && endDepot.value > end.value
    ? null :{ depotDateValid:true };
      }