import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './modules/auth/components/auth-callback.component';
import { LoginRedirectComponent } from './modules/auth/components/login-redirect/login-redirect.component';
import {HomeComponent} from './modules/home/home.component'
import { ProfileModule } from './modules/profile/profile.module';

const routes: Routes = [
  { path: '', loadChildren: './modules/home/home.module#HomeModule' },
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'unauthorized', component: LoginRedirectComponent, },
  { path: 'jamevent', loadChildren: './modules/jam-event/jam-event.module#JamEventModule' },
  { path: 'profile', loadChildren: './modules/profile/profile.module#ProfileModule' },
  { path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
