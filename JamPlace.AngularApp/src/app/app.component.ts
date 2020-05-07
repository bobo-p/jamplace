import { Component,ChangeDetectorRef } from '@angular/core';
import { AuthService } from './modules/auth/services/auth.service';
import {HomeService} from './modules/home/services/home.service';
import {MediaMatcher} from '@angular/cdk/layout';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'JamPlace';
  text: string;
  showNavbar = false;
  mobileQuery: MediaQueryList;
  private _mobileQueryListener: () => void;
  loading: boolean;
  constructor(
    media: MediaMatcher,
    changeDetectorRef: ChangeDetectorRef,
    public authService: AuthService,   
    private homeService: HomeService
   ) { 
    this.loading = true;
    this.mobileQuery = media.matchMedia('(max-width: 650px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
   }

   ngOnInit() {
    
    this.authService.initAuth();
    // if (!this.authService.isAuthorized)
    // {
    //   window.location.href="http://localhost:5006/Identity/Account/Login";
    // }
    this.text="Aaaaa";
    const tryGetMail = setInterval(() => {
      if (this.authService.isAuthorized) {
        this.homeService.getTestString().then(data => {
          this.text = data;
          console.log(data);
          this.loading = false;
          this.showNavbar = true;
        }, fail => {
          
          console.log(fail);
          this.loading = false;
        });
        clearInterval(tryGetMail);
      }
      else{
        this.authService.login();
      }
      
    }, 500);
   
   }

  ngOnDestroy(): void {
   // this.mobileQuery.removeListener(this._mobileQueryListener);
    this.authService.ngOnDestroy();
  }

}
