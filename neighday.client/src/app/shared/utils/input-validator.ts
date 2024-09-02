import { AbstractControl, ValidationErrors } from '@angular/forms';

export class InputValidator {
  static noLeadingSpace(control: AbstractControl): ValidationErrors | null {
    const text = control.value as string;
    if (text.startsWith(' ')) {
      return { leadingSpace: true };
    }

    return null;
  }
}
