import { Component, OnInit } from '@angular/core';
import { UserEventsService } from '../../services/user-events.service';
import { UserSpecificJamEvent } from '../../models/UserSpecificJamEvent';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-events-list',
  templateUrl: './my-events-list.component.html',
  styleUrls: ['./my-events-list.component.scss']
})
export class MyEventsListComponent implements OnInit {

  private myEvents: UserSpecificJamEvent[];
  constructor( 
    private userJamEventsService: UserEventsService,
    private router: Router,
    private r:ActivatedRoute ) { }

  ngOnInit() {
    this.userJamEventsService.getCurrentUserEventevents().then(result => {
        this.myEvents=result;     
      },
       error => {
        console.log(error);
    }); 
  }

  onSelect(myJamEvent: UserSpecificJamEvent): void {
    this.router.navigate(['jamevent/main-event',myJamEvent.id],{ relativeTo: this.r.parent });
  }

}
