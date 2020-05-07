import { Component, OnInit, Input } from '@angular/core';
import { JamUserViewModel } from '../../models/jam-user-viewmodel';
import { BehaviorSubject } from 'rxjs';
import { JamUserService } from '../../services/jam-user.service';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { LoggedUserService } from 'src/app/modules/shared/services/logged-user.service';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from 'src/app/modules/shared/components/confirm-dialog/confirm-dialog.component';
import { JamEventService } from '../../services/jam-event.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  
  @Input('event-id') eventId: number;
  @Input('user-list') userList: JamUserViewModel[];
  @Input('event-owner-id') eventOwnerId: number;

   users$: BehaviorSubject<JamUserViewModel[]>;
   searchTerms: BehaviorSubject <string>;
   firstSearch: boolean;
   listEmpty: boolean;
   loggedUserId: number;
  constructor(private jamUserService : JamUserService,
    private loggedUserService: LoggedUserService,
    public dialog: MatDialog,
    private jamEventService : JamEventService,
    private router: Router) { 
      this.loggedUserId= this.loggedUserService.getCurrentLoggedUser().id;
    }

  ngOnInit() {
    this.firstSearch = true;
    this.sortArrayWithCurrentUserOnTop();
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
  deleteEvent(): void
  {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Na pewno chcesz usunąc to wydarzenie?",
          message: "Potwierdzenie poskutkuje nieodwracalnym usunięciem tego wydarzenia wraz z danymi!"}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
     if(dialogResult)
     {
        this.jamEventService.deleteJamEevent(this.eventId).then(response => {   
          this.router.navigate(['/']);
          },
           error => {
            console.log(error);
        }); 
     }      
   });
  }

  sortArrayWithCurrentUserOnTop() {
    var currentUserInList = this.userList.filter(item => item.id === this.loggedUserId)[0];
    this.userList = this.userList.filter(item => item.id !== this.loggedUserId);
    this.userList.unshift(currentUserInList);
  }

}
