import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from './features/account/services/auth.service';
import { OnlineService } from './shared/services/online.service';
import { AsyncPipe } from '@angular/common';
import { ChatComponent } from './features/chathub/components/chat/chat.component';
import { first } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, ChatComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  protected readonly authService = inject(AuthService);
  protected readonly onlineService = inject(OnlineService);

  ngOnInit() {
    this.authService
      .getUser()
      .pipe(first())
      .subscribe((user) => {
        if (user) {
          this.onlineService.startConnection().then(() => {
            this.onlineService.updateOnlineUsers();
          });
        }
      });
  }

  logout() {
    this.authService
      .logout()
      .pipe(first())
      .subscribe(() => {
        location.href = '/login';
      });
  }
}
