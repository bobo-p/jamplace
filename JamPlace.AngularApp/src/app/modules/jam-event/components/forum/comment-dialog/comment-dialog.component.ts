import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CommentViewModel } from '../../../models/comment-viewmodel';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { CommentService } from '../../../services/comment.service';
@Component({
  selector: 'app-comment-dialog',
  templateUrl: './comment-dialog.component.html',
  styleUrls: ['./comment-dialog.component.scss']
})
export class CommentDialogComponent implements OnInit {
  
  private commentForm: FormGroup;
  private submitted = false;
  private currentComment: CommentViewModel;
  private isEditingMode: boolean;
  constructor(
    public dialogRef: MatDialogRef<CommentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public comment: CommentViewModel,
    private commentService: CommentService) {
     this.currentComment = comment;
     this.commentForm = this.createFormGroup();
     if(comment.id)
      this.isEditingMode = true;
    }

    onNoClick(): void {
      this.dialogRef.close();
    }
    ngOnInit(): void {
  
    }
    onSubmit() {
      this.submitted=true;
        if (this.commentForm.invalid ) {
          return;
      }
      var result = this.currentComment;
      result.content =  this.commentForm.value.content;
      result.eventId = this.currentComment.eventId;
      if(!this.isEditingMode) {
        this.commentService.addComment(result).then(response => {   
          result = response;
          this.dialogRef.close(result);
          },
          error => {
            console.log(error);
            this.submitted=false;
        });
        return;
      }
      result.id = this.currentComment.id;
      this.commentService.updateComment(result).then(response => {   
        this.dialogRef.close(result);
        },
        error => {
          console.log(error);
          this.submitted=false;
      });
    }

  private createFormGroup() {
    if(!this.currentComment.id) {
      return new FormGroup({
        content: new FormControl('',[Validators.required]),
      
      });
    }
    var formGroup = new FormGroup({
      content: new FormControl(this.currentComment.content,[Validators.required]),         
    });

    return formGroup;
  }
  get content() {
    return this.commentForm.get('content');
  }
  get f() { return this.commentForm.controls; }

}
