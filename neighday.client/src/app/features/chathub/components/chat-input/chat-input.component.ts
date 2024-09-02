import { Component, EventEmitter, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../account/services/auth.service';
import { AsyncPipe } from '@angular/common';
import { InputValidator } from '../../../../shared/utils/input-validator';

@Component({
  selector: 'app-chat-input',
  standalone: true,
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './chat-input.component.html',
  styleUrl: './chat-input.component.css',
})
export class ChatInputComponent {
  private readonly formBuilder = inject(FormBuilder);
  public readonly authService = inject(AuthService);
  @Output() sendMessageEvent = new EventEmitter<string>();

  readonly form: FormGroup = this.formBuilder.group({
    text: ['', [Validators.required, Validators.maxLength(500), InputValidator.noLeadingSpace]],
  });

  sendMessage(ev: Event) {
    ev.preventDefault();

    const text = this.form.value['text'] as string;
    this.sendMessageEvent.emit(text);

    this.form.patchValue({ text: '' });
  }
}
