import { Component, OnInit, Input } from '@angular/core';
import { JamUserViewModel } from '../../models/jam-user-viewmodel';
import { BehaviorSubject } from 'rxjs';
import { JamUserService } from '../../services/jam-user.service';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  
  @Input('event-id') eventId: number;
  @Input('user-list') userList: JamUserViewModel[];

  private users$: BehaviorSubject<JamUserViewModel[]>;
  private searchTerms: BehaviorSubject <string>;
  private firstSearch: boolean;
  private listEmpty: boolean;

  constructor(private jamUserService : JamUserService) { }

  ngOnInit() {
    this.firstSearch = true;
    this.users$ = new BehaviorSubject<JamUserViewModel[]>(this.userList);
    if(!this.userList || this.userList.length === 0) {
      this.listEmpty=true;
    }
  }
  search(term: string): void {

    if(this.firstSearch)
    {     

      this.searchTerms = new BehaviorSubject<string>(term);
      var obs = this.searchTerms.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.jamUserService.search(term))
      );
      obs.subscribe(items => {
        this.userList = items;
        if(this.userList.length === 0)
          this.listEmpty=true;
        else
          this.listEmpty=false;
        this.users$.next(this.userList);
      });
      this.firstSearch = false;
    }
    else {
      this.searchTerms.next(term);
    }   
  }

}
