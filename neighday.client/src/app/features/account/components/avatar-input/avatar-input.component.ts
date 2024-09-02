import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Avatar } from '../../models/avatar.model';

@Component({
  selector: 'app-avatar-input',
  standalone: true,
  imports: [],
  templateUrl: './avatar-input.component.html',
  styleUrl: './avatar-input.component.css',
})
export class AvatarInputComponent implements OnInit {
  @Input() avatarUrl?: string;
  @Output() changeEvent = new EventEmitter<void>();
  avatars!: Avatar[];

  ngOnInit() {
    this.initAvatars();
  }

  private initAvatars() {
    this.avatars = [
      {
        name: 'avatar1',
        url: '/assets/avatars/marble-avatar.png',
      },
      {
        name: 'avatar2',
        url: '/assets/avatars/marble-avatar.png',
      },
    ];
  }
}
