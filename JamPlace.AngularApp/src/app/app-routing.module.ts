import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './modules/auth/components/auth-callback.component';
import { LoginRedirectComponent } from './modules/auth/components/login-redirect/login-redirect.component';
import {HomeComponent} from './modules/home/home.component'

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'unauthorized', component: LoginRedirectComponent, },
  { path: 'jamevent', loadChildren: './modules/jam-event/jam-event.module#JamEventModule' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
