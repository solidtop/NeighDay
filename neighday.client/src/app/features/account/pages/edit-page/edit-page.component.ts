import { Component, ViewChild, inject } from '@angular/core';
import { DisplayColorInputComponent } from '../../components/display-color-input/display-color-input.component';
import { AvatarInputComponent } from '../../components/avatar-input/avatar-input.component';
import { AuthService } from '../../services/auth.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-edit-page',
  standalone: true,
  imports: [AsyncPipe, AvatarInputComponent, DisplayColorInputComponent],
  templateUrl: './edit-page.component.html',
  styleUrl: './edit-page.component.css',
})
export class EditPageComponent {
  public readonly authService = inject(AuthService);
  @ViewChild(DisplayColorInputComponent) displayColorInput!: DisplayColorInputComponent;

  hasChanges = false;

  handleChanges() {
    this.hasChanges = true;
    console.log('change');
  }

  saveChanges() {
    this.hasChanges = false;
    console.log(this.displayColorInput.displayColor);
  }
}
