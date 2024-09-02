import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChatChannel } from '../../models/chat-channel.model';

@Component({
  selector: 'app-channel-input',
  standalone: true,
  imports: [],
  templateUrl: './channel-input.component.html',
  styleUrl: './channel-input.component.css',
})
export class ChannelInputComponent {
  @Input() channels!: ChatChannel[];
  @Input() currentChannel!: ChatChannel;
  @Output() changeChannelEvent = new EventEmitter<ChatChannel>();

  changeChannel(channel: ChatChannel) {
    this.changeChannelEvent.emit(channel);
  }
}
