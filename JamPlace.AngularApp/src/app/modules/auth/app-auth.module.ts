import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthCallbackComponent } from './components/auth-callback.component';
import { OidcSecurityService, AuthModule } from 'angular-auth-oidc-client';
import { RouterModule } from '@angular/router';





@NgModule({
  declarations: [AuthCallbackComponent],
  imports: [
    CommonModule,
    RouterModule,
    AuthModule.forRoot()
  ],
  providers: [
    OidcSecurityService
  ]
})
export class AppAuthModule { }
