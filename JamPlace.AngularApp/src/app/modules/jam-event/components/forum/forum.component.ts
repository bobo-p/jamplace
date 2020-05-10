import { Component, OnInit, Input } from '@angular/core';
import { CommentViewModel } from '../../models/comment-viewmodel';
import { BehaviorSubject } from 'rxjs';
import { CommentSearchRequest } from '../../models/comment-search-request';
import { CommentService } from '../../services/comment.service';
import { CommentDialogComponent } from './comment-dialog/comment-dialog.component';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from 'src/app/modules/shared/components/confirm-dialog/confirm-dialog.component';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.scss']
})
export class ForumComponent implements OnInit {

  @Input('event-id') eventId: number;
  @Input('comment-list') commentist: CommentViewModel[];

   comments$: BehaviorSubject<CommentViewModel[]>;
  private searchTerms: BehaviorSubject <string>;
   firstSearch: boolean;
   listEmpty: boolean;
   searchRequest: CommentSearchRequest;

  constructor(public dialog: MatDialog,
    private commentService: CommentService) { }

  ngOnInit() {
    this.firstSearch = true;
    console.log(this.commentist);
    this.comments$ = new BehaviorSubject<CommentViewModel[]>(this.commentist);
    if(!this.commentist || this.commentist.length === 0) {
      this.listEmpty=true;
    }
  }

  openDialog(model?: CommentViewModel): void {
    if(!model)
      model = new CommentViewModel();
      
    model.eventId = this.eventId;
    const dialogRef = this.dialog.open(CommentDialogComponent, {
      width: '450px',
      data: model
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.listEmpty=false;
        this.updateCommentList(result);
        this.comments$.next(this.commentist);
      }
    });
  }
  deleteComment(comment: CommentViewModel): void
  {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Usuwanie komentarza",
          message: "Potwierdź usunięcie komentarza"}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
     if(dialogResult)
     {
        this.commentService.deleteComment(comment).then(response => {   
          this.commentist = this.commentist.filter(item => item !== comment);
          if(this.commentist.length === 0)
            this.listEmpty=true;
          this.comments$.next(this.commentist);
          },
           error => {
            console.log(error);
        }); 
     }      
   });
  }
  search(term: string): void {

    if(this.firstSearch)
    {     
      this.searchRequest = new CommentSearchRequest(term, this.eventId);
      this.searchTerms = new BehaviorSubject<string>(term);
      var obs = this.searchTerms.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.commentService.search(this.searchRequest))
      );
      obs.subscribe(items => {
        this.commentist = items;
        if(this.commentist.length === 0)
          this.listEmpty=true;
        else
          this.listEmpty=false;
        this.comments$.next(this.commentist);
      });
      this.firstSearch = false;
    }
    else {
      this.searchRequest.searchText = term;
      this.searchTerms.next(term);
    }   
  }
  private updateCommentList(newItem){

    let updateItem = this.commentist.find(this.findIndexToUpdate, newItem.id);
    if(!updateItem)
    {
      this.commentist.unshift(newItem);
      return;
    }
    let index = this.commentist.indexOf(updateItem);
    this.commentist[index] = newItem;

  }
  private findIndexToUpdate(newItem) { 
    return newItem.id === this;
}

}
