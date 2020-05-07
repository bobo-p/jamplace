import { Component, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { UserSpecificJamEvent } from 'src/app/modules/home/models/UserSpecificJamEvent';
import { UserEventsService } from 'src/app/modules/home/services/user-events.service';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { JamEventService } from '../../services/jam-event.service';
import { JamEventViewModel } from '../../models/jam-event-viewmodel';
import { JamEventSearchRequest } from '../../models/jam-event-search-request';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-event',
  templateUrl: './search-event.component.html',
  styleUrls: ['./search-event.component.scss']
})
export class SearchEventComponent implements OnInit {

  private myEvents$: Observable<JamEventViewModel[]>;
  private searchTerms: BehaviorSubject <string>;
  private firstSearch: boolean;
  
  constructor(private jamEventsService: JamEventService,
    private router: Router, private r:ActivatedRoute) { }

  ngOnInit() {
    this.firstSearch = true;
    this.jamEventsService.getJamEevents().then(result => {
        this.myEvents$ = new Observable<JamEventViewModel[]>(observer => observer.next(result));   
      },
       error => {
        console.log(error);
    }); 
  }
  onJoin(model: JamEventViewModel){
    console.log(model);
    this.jamEventsService.joinJamEvent(model.id).then(result => {
      this.router.navigate(['main-event',model.id],{ relativeTo: this.r.parent });   
    },
     error => {
      console.log(error);
  }); 
  }
  search(term: string): void {

    var request: JamEventSearchRequest = {
      name: term,
      city: '',
      ownerName: ''
    }
    if(this.firstSearch)
    {
      this.searchTerms = new BehaviorSubject<string>(term);
      this.myEvents$ = this.searchTerms.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.jamEventsService.search(request)),
      );
      this.firstSearch = false;
    }
    else
      this.searchTerms.next(term);
  }

}
