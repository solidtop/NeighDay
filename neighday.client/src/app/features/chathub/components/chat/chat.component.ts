import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
  inject,
} from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ChatInputComponent } from '../chat-input/chat-input.component';
import { ChatChannel } from '../../models/chat-channel.model';
import { StorageService } from '../../../../shared/services/storage.service';
import { DateService } from '../../../../shared/services/date.service';
import { ChannelInputComponent } from '../channel-input/channel-input.component';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, ChatInputComponent, ChannelInputComponent, AsyncPipe],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit, OnDestroy, AfterViewInit {
  public readonly chatService = inject(ChatService);
  private readonly storageService = inject(StorageService);
  public readonly dateService = inject(DateService);

  channels: ChatChannel[] = [];
  currentChannel!: ChatChannel;
  channelChanged = false;

  @ViewChild('scrollFrame', { static: false }) scrollFrame?: ElementRef<HTMLElement>;
  @ViewChildren('item') itemElements?: QueryList<HTMLElement>;

  ngOnInit() {
    this.chatService.startConnection().then(() => {
      this.chatService.getChannels().subscribe((channels) => {
        this.channels = channels;
        this.currentChannel = this.storageService.getLocal<ChatChannel>('channel-input') || channels[0];
        this.joinChannel(this.currentChannel);
      });
    });
  }

  ngOnDestroy() {
    this.chatService.stopConnection();
  }

  ngAfterViewInit() {
    this.itemElements?.changes.subscribe(() => {
      if (this.isNearBottom() || this.channelChanged) {
        this.scrollToBottom();
        this.channelChanged = false;
      }
    });
  }

  joinChannel(channel: ChatChannel) {
    this.currentChannel = channel;
    this.storageService.setLocal('channel-input', this.currentChannel);
    this.chatService.joinChannel(channel.id);
    this.channelChanged = true;
  }

  sendMessage(text: string) {
    this.chatService.sendMessage({
      text,
      channelId: this.currentChannel.id,
    });
  }

  isNearBottom() {
    const frame = this.scrollFrame?.nativeElement;
    if (!frame) return false;

    const threshhold = 150;
    const position = frame.scrollTop + frame?.offsetHeight;
    const height = frame.scrollHeight;
    return position > height - threshhold;
  }

  scrollToBottom() {
    const frame = this.scrollFrame?.nativeElement;
    if (!frame) return;

    frame.scroll({
      top: frame.scrollHeight,
    });
  }
}
