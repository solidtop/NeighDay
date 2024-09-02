import { ChatPageComponent } from './features/chathub/pages/chat-page/chat-page.component';
import { RegisterPageComponent } from './features/account/pages/register-page/register-page.component';
import { LoginPageComponent } from './features/account/pages/login-page/login-page.component';
import { Routes } from '@angular/router';
import { authGuard } from './features/account/guards/auth.guard';
import { AccountComponent } from './features/account/components/account/account.component';
import { EditPageComponent } from './features/account/pages/edit-page/edit-page.component';

export const routes: Routes = [
  { path: '', redirectTo: 'chat', pathMatch: 'full' },
  { path: 'chat', component: ChatPageComponent, canActivate: [authGuard] },
  { path: 'register', component: RegisterPageComponent },
  { path: 'login', component: LoginPageComponent },
  {
    path: 'account',
    component: AccountComponent,
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'edit', pathMatch: 'full' },
      { path: 'edit', component: EditPageComponent },
    ],
  },
];
