import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { CommentViewModel } from '../models/comment-viewmodel';
import { CommentSearchRequest } from '../models/comment-search-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

  public addComment(comment: CommentViewModel): Promise<any> {
    return this.authService.post(this.api + '/Comment/AddComment', comment, 'application/json; charset=utf-8');
  }
  public updateComment(comment: CommentViewModel): Promise<any> {
    return this.authService.post(this.api + '/Comment/UpdateComment', comment, 'application/json; charset=utf-8');
  }
  public deleteComment(comment: CommentViewModel): Promise<any> {
    return this.authService.post(this.api + '/Comment/DeleteComment', comment, 'application/json; charset=utf-8');
  }
  public search(request: CommentSearchRequest): Observable<CommentViewModel[]> {
    return this.authService.postAsObservable(this.api + '/Comment/SearchComment/', request, 'application/json; charset=utf-8');
  }
}
