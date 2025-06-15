import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { ColleagueListComponent } from './features/colleagues/colleague-list/colleague-list.component';
import { ColleagueDetailComponent } from './features/colleagues/colleague-detail/colleague-detail.component';
import { ConnectionsComponent } from './features/connections/connections.component';
import { MessagesComponent } from './features/messages/messages.component';
import { authGuard } from './core/guards/auth.guard';
import { TestErrorsComponent } from './features/test-errors/test-errors.component';
import { NotFoundComponent } from './shared/errors/not-found/not-found.component';
import { ServerErrorComponent } from './shared/errors/server-error/server-error.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'colleagues', component: ColleagueListComponent, canActivate: [authGuard] },
      { path: 'colleagues/:id', component: ColleagueDetailComponent },
      { path: 'connections', component: ConnectionsComponent },
      { path: 'messages', component: MessagesComponent },
    ]
  },
  { path: 'test-errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
