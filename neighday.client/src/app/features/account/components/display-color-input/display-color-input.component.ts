import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-display-color-input',
  standalone: true,
  imports: [],
  templateUrl: './display-color-input.component.html',
  styleUrl: './display-color-input.component.css',
})
export class DisplayColorInputComponent {
  @Input() displayColor?: string;
  @Output() changeEvent = new EventEmitter<void>();
}
