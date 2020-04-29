import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { JamEventInfo } from '../../shared/jam-event-info';
import { JamEventViewModel } from '../models/jam-event-viewmodel';
@Injectable({
  providedIn: 'root'
})
export class JamEventService {
  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

  public addJamEvent(jamEventInfo: JamEventInfo): Promise<any> {
    return this.authService.post(this.api + '/JamEvent/AddJamEvent', jamEventInfo, 'application/json; charset=utf-8');
  }

  public getJamEevent(eventId: number): Promise<JamEventViewModel> {
    return this.authService.get(this.api + '/JamEvent/GetEvent/' + eventId);
  }
}
