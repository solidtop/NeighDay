import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiService } from '../../../shared/services/api.service';
import { ChatMessage } from '../models/chat-message.model';
import { ChatMessageRequest } from '../models/chat-message-request.model';
import { ChatChannel } from '../models/chat-channel.model';

@Injectable({
  providedIn: 'root',
})
export class ChatService extends ApiService {
  private connection: HubConnection;
  messages$ = new BehaviorSubject<ChatMessage[]>([]);

  constructor() {
    super();

    this.connection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/chat`, {
        skipNegotiation: true,
        withCredentials: true,
        transport: HttpTransportType.WebSockets,
      })
      .build();
  }

  async startConnection() {
    try {
      await this.connection.start();
    } catch (error) {
      console.log('Error connecting to hub:', error);
    }

    this.connection.on('ReceiveMessage', (message: ChatMessage) => {
      const currentMessages = this.messages$.getValue();
      this.messages$.next([...currentMessages, message]);
    });
  }

  async stopConnection() {
    await this.connection?.stop();
  }

  getChatHistory(channelId: string) {
    return this.http.get<ChatMessage[]>(`${this.apiUrl}/api/chat?channelId=${channelId}`, {
      headers: this.headers,
      withCredentials: true,
    });
  }

  getChannels() {
    return this.http.get<ChatChannel[]>(`${this.apiUrl}/api/chat/channels`, {
      headers: this.headers,
      withCredentials: true,
    });
  }

  joinChannel(channelId: string) {
    this.connection?.invoke('JoinChannel', channelId).then((messages: ChatMessage[]) => {
      this.messages$.next(messages);
    });
  }

  leaveChannel(channelId: string) {
    this.connection?.send('LeaveChannel', channelId);
  }

  sendMessage(request: ChatMessageRequest) {
    this.connection?.send('SendMessage', request);
  }
}
