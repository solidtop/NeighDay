import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { UserSummary } from '../../features/account/models/user-summary.model';

@Injectable({
  providedIn: 'root',
})
export class OnlineService extends ApiService {
  private connection: HubConnection;
  onlineUsers$ = new BehaviorSubject<UserSummary[]>([]);

  constructor() {
    super();

    this.connection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/online`, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .build();
  }

  async startConnection() {
    try {
      return await this.connection.start();
    } catch (error) {
      console.log(error);
    }
  }

  updateOnlineUsers() {
    this.connection?.on('UpdateOnlineUsers', (users: UserSummary[]) => {
      this.onlineUsers$.next(users);
      this.connection?.off('UpdateOnlineUsers'); // Handle can be removed after getting the initial users
    });

    this.connection?.on('AddOnlineUser', (user: UserSummary) => {
      const currentUsers = this.onlineUsers$.getValue();
      this.onlineUsers$.next([...currentUsers, user]);
    });

    this.connection?.on('RemoveOnlineUser', (userId: string) => {
      const currentUser = this.onlineUsers$.getValue();
      this.onlineUsers$.next(currentUser.filter((u) => u.id !== userId));
    });
  }
}
