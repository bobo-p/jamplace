import { Component, OnInit } from '@angular/core';
import { UserEventsService } from '../../services/user-events.service';
import { UserSpecificJamEvent } from '../../models/UserSpecificJamEvent';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';

@Component({
  selector: 'app-my-events-list',
  templateUrl: './my-events-list.component.html',
  styleUrls: ['./my-events-list.component.scss']
})
export class MyEventsListComponent implements OnInit {

   myEvents$: Observable<UserSpecificJamEvent[]>;
  private searchTerms: BehaviorSubject <string>;
   firstSearch: boolean;

  constructor( 
    private userJamEventsService: UserEventsService,
    private router: Router,
    private r:ActivatedRoute ) { }

  ngOnInit() {
    this.firstSearch = true;
    this.userJamEventsService.getCurrentUserEventevents().then(result => {
        this.myEvents$ = new Observable<UserSpecificJamEvent[]>(observer => observer.next(result));   
      },
       error => {
        console.log(error);
    }); 
  }

  onSelect(myJamEvent: UserSpecificJamEvent): void {
    this.router.navigate(['jamevent/main-event',myJamEvent.id],{ relativeTo: this.r.parent });
  }

  search(term: string): void {
    if(this.firstSearch)
    {
      this.searchTerms = new BehaviorSubject<string>(term);
      this.myEvents$ = this.searchTerms.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.userJamEventsService.searchByName(term)),
      );
      this.firstSearch = false;
    }
    else
      this.searchTerms.next(term);
  }

}
