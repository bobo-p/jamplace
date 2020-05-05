import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { JamEventInfo } from '../../shared/jam-event-info';
@Injectable({
  providedIn: 'root'
})
export class JamEventInfoService {
  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

  public updateJamEvent(jamEventInfo: JamEventInfo): Promise<any> {
    return this.authService.post(this.api + '/JamEvent/UpdateJamEvent', jamEventInfo, 'application/json; charset=utf-8');
  }
}
