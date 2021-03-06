import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JamUserModel } from '../shared/jam-user-info';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

 
  constructor(private router: Router) { }

  ngOnInit() {

  }
  clickAddEvent(){
    this.router.navigate(['/jamevent/addJamEvent']);
  }
  clickSearchEvent(){
    this.router.navigate(['/jamevent/searchJamEvent']);
  }
  clickProfile(){
    this.router.navigate(['/profile']);
    
  }
}
