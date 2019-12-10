import { Component, OnInit, OnDestroy } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  template: '',
  styleUrls: []
})
export class AuthCallbackComponent implements OnInit, OnDestroy {

  constructor(
    private _location: Location,
    private _router: Router
  ) { }

  ngOnInit() {
    if ('returnUrl' in localStorage) {
      this._location.replaceState(localStorage.getItem('returnUrl'));
    }
  }

  ngOnDestroy() {
    if ('returnUrl' in localStorage) {
      this._router.navigate([localStorage.getItem('returnUrl')], { replaceUrl: true });
      localStorage.removeItem('returnUrl');
    }
  }

}
