import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './modules/auth/components/auth-callback.component';
import { LoginRedirectComponent } from './modules/auth/components/login-redirect/login-redirect.component';

const routes: Routes = [
  { path: '', loadChildren: './modules/home/home.module#HomeModule' },
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'unauthorized', component: LoginRedirectComponent, },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
