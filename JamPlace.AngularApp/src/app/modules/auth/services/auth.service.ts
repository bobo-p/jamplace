import { Injectable, Inject } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { throwError, Subscription, Observable } from 'rxjs';
import { catchError, debounceTime } from 'rxjs/operators';
import { Router } from '@angular/router';
import { OidcSecurityService, OpenIdConfiguration, AuthWellKnownEndpoints, AuthorizationResult, AuthorizationState } from 'angular-auth-oidc-client';
import * as M from "materialize-css/dist/js/materialize";

@Injectable()
export class AuthService {
  isAuthorized = false;
  private isAuthorizedSubscription: Subscription = new Subscription();
  private afterAuthorizationSubscription: Subscription = new Subscription();

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private http: HttpClient,
    private _router: Router,
    @Inject('BASE_URL') private originUrl,
    @Inject('AUTH_URL') private authUrl
  ) { }

  ngOnDestroy(): void {
    if (this.isAuthorizedSubscription) {
      this.isAuthorizedSubscription.unsubscribe();
    }
    if (this.afterAuthorizationSubscription) {
      this.afterAuthorizationSubscription.unsubscribe();
    }
  }

  public initAuth() {
    const openIdConfiguration: OpenIdConfiguration = {
      stsServer: this.authUrl,
      redirect_url: this.originUrl + 'auth-callback',
      client_id: 'JamPlace',
      response_type: 'code',
      scope: 'openid profile email JamPlaceApi',
      post_logout_redirect_uri: this.originUrl,
      forbidden_route: '/forbidden',
      unauthorized_route: '/unauthorized',
      silent_renew: true,
      silent_renew_url: this.originUrl + 'silent-renew.html',
      history_cleanup_off: false,
      auto_userinfo: true,
      log_console_warning_active: false,
      log_console_debug_active: false,
      disable_iat_offset_validation: true,
      max_id_token_iat_offset_allowed_in_seconds: 10
    };
    const authWellKnownEndpoints: AuthWellKnownEndpoints = {
      issuer: this.authUrl,
      jwks_uri: this.authUrl + '/.well-known/openid-configuration/jwks',
      authorization_endpoint: this.authUrl + '/connect/authorize',
      token_endpoint: this.authUrl + '/connect/token',
      userinfo_endpoint: this.authUrl + '/connect/userinfo',
      end_session_endpoint: this.authUrl + '/connect/endsession',
      check_session_iframe: this.authUrl + '/connect/checksession',
      revocation_endpoint: this.authUrl + '/connect/revocation',
      introspection_endpoint: this.authUrl + '/connect/introspect',
    };

    this.oidcSecurityService.setupModule(openIdConfiguration, authWellKnownEndpoints);
    
    if (this.oidcSecurityService.moduleSetup) {
      this.doCallbackLogicIfRequired();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.doCallbackLogicIfRequired();
      });
    }
    this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe((isAuthorized => {
      this.isAuthorized = isAuthorized;
     
    }));
    
    this.afterAuthorizationSubscription = this.oidcSecurityService.onAuthorizationResult.subscribe(
      (authorizationResult: AuthorizationResult) => {
       
        this.onAuthorizationResultComplete(authorizationResult);
      });
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  getIsAuthorized(): Observable<boolean> {
    return this.oidcSecurityService.getIsAuthorized();
  }

  getHeaders(contentType: string = null): HttpHeaders {
    let headers = new HttpHeaders();

    if (contentType) {
      headers = headers.set('Content-Type', contentType);
    }

    let accessToken = this.oidcSecurityService.getToken();

    if (accessToken === '') {
      return headers;
    }

    if (accessToken === '' || !accessToken || accessToken === undefined || accessToken === null) {
      return headers;
    }

    let token = `Bearer ${accessToken}`;
    headers = headers.set('Authorization', token);

    return headers;
  }

  get(url: string): Promise<any> {
    return this.http.get(url, { headers: this.getHeaders() })
      .pipe(catchError((err) => {
        this.handleError(err);
        return throwError(err);
      })).toPromise();
  }

  getAsObservable(url: string): Observable<any> {
    return this.http.get(url, { headers: this.getHeaders() })
      .pipe(catchError((err) => {
        this.handleError(err);
        return throwError(err);
      }));
  }

  put(url: string, data: any, contentType: string = null): Promise<any> {
    const body = JSON.stringify(data);
    return this.http.put(url, body, { headers: this.getHeaders(contentType) })
      .pipe(catchError((err) => {
        this.handleError(err);
        return throwError(err);
      })).toPromise();
  }

  delete(url: string): Promise<any> {
    return this.http.delete(url, { headers: this.getHeaders() })
      .pipe(catchError((err) => {
        this.handleError(err);
        return throwError(err);
      })).toPromise();
  }

  post(url: string, data: any, contentType: string = null): Promise<any> {
    contentType = contentType ? contentType : '';
    const body = contentType.includes('application/json') ? JSON.stringify(data) : data;
    return this.http.post(url, body, { headers: this.getHeaders(contentType) })
      .pipe(catchError((err) => {
        this.handleError(err);
        debounceTime(300);
        return throwError(err);
      })).toPromise();
  }

  postAsObservable(url: string, data: any, contentType: string = null): Observable<any> {
    contentType = contentType ? contentType : '';
    const body = contentType.includes('application/json') ? JSON.stringify(data) : data;
    return this.http.post(url, body, { headers: this.getHeaders(contentType) })
      .pipe(catchError((err) => {
        this.handleError(err);
        debounceTime(300);
        return throwError(err);
      }));
  }
  private handleError(err: any) {
    
    switch (err.status) {
      case 0:
        this._router.navigate(['not-response']);
        break;
      case 401:
        let path = window.location.pathname;
        if (path !== '/unauthorized' && !('returnUrl' in localStorage)) {
          localStorage.setItem('returnUrl', path);
        }
        //window.location.href = '/unauthorized';
        this.login();
        break;
      case 403:
        M.toast({html: 'Brak dostępu do zasobu!',displayLength: 1500,classes: 'rounded'})
        this._router.navigate(['/']);
        break;
      case 404:
        M.toast({html: 'Nie znaleziono zasobu!',displayLength: 1500,classes: 'rounded'})
        this._router.navigate(['/not-found'])
        break;      
      case 500:
        M.toast({html: 'Błąd serwera podczas wykonywania zapytania!',displayLength: 1500,classes: 'rounded'})
        this._router.navigate(['/internal-error']);
        break;
    }
  }

  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
    
    if (authorizationResult.authorizationState === AuthorizationState.unauthorized) {
      
      if (window.parent) {
        // sent from the child iframe, for example the silent renew
        this._router.navigate(['/unauthorized']);
      } else {
        window.location.href = '/unauthorized';
      }
    }
  }

  private doCallbackLogicIfRequired() {
    this.oidcSecurityService.authorizedCallbackWithCode(window.location.toString());
  }

}
