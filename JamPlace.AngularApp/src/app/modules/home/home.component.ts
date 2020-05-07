import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JamUserModel } from '../shared/jam-user-info';
import { HomeService } from './services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  private jamUserInfo: JamUserModel;

  constructor(private router: Router,
    private homeService: HomeService) { }

  ngOnInit() {
    this.homeService.getJamUserData().then( user => {     
      this.jamUserInfo = user;
      },
       error => {
        console.log(error);
    });
  }
  clickAddEvent(){
    this.router.navigate(['/jamevent/addJamEvent']);
  }
  clickSearchEvent(){
    this.router.navigate(['/jamevent/searchJamEvent']);
  }
}
