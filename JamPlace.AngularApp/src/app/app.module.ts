import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AuthService } from './modules/auth/services/auth.service';
import { AppRoutingModule } from './app-routing.module';
import { AppAuthModule } from './modules/auth/app-auth.module';
import {HomeModule} from './modules/home/home.module';
import { AppComponent } from './app.component';
import { LoginRedirectComponent } from './modules/auth/components/login-redirect/login-redirect.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './modules/material/material.module';
import { HomeComponent } from './modules/home/home.component';
export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

@NgModule({
  declarations: [
    AppComponent,
    LoginRedirectComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppAuthModule,
    //HomeModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,

  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
     { provide: 'AUTH_URL', useValue: 'http://localhost:5005' },
     { provide: 'API_URL', useValue: 'http://localhost:26001/api' },
    // { provide: 'AUTH_URL', useValue: 'https://login-alfa.jamplace.local:81' },
    // { provide: 'API_URL', useValue: 'https://alfa-api.jamplace.local:81/api' },
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
