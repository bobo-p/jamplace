import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { UserSpecificJamEvent } from '../models/UserSpecificJamEvent';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserEventsService {

  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

    public getCurrentUserEventevents(): Promise<UserSpecificJamEvent[]> {
      return this.authService.get(this.api + '/JamEvent/GetCurrentUserEvents/');
    }

    public searchByName(name: string): Observable<UserSpecificJamEvent[]> {
      return this.authService.getAsObservable(this.api + '/JamEvent/SearchUserEventsByName/' + name);
    }
}
