import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { UserSpecificJamEvent } from '../models/UserSpecificJamEvent';

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
}
