import { Component } from '@angular/core';
import { AuthService } from './modules/auth/services/auth.service';
import {HomeService} from './modules/home/services/home.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'JamPlace';
  text: string;
  showNavbar = false;

  constructor(
    public authService: AuthService,   
    private homeService: HomeService
   ) { }

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
          this.showNavbar = true;
        }, fail => {
          
          console.log(fail);
        });
        clearInterval(tryGetMail);
      }
      
    }, 500);
   
   }

  ngOnDestroy(): void {
   // this.mobileQuery.removeListener(this._mobileQueryListener);
    this.authService.ngOnDestroy();
  }

}
