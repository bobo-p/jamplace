import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { Observable } from 'rxjs';
import { JamUserViewModel } from '../models/jam-user-viewmodel';

@Injectable({
  providedIn: 'root'
})
export class JamUserService {

  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

  public search(request: string): Observable<JamUserViewModel[]> {
    return this.authService.postAsObservable(this.api + '/JamEvent/Search/', request, 'application/json; charset=utf-8');
  }
}
