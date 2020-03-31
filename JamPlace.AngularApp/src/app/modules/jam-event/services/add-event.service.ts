import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import {JamEventInfo} from '../models/jam-event-info'
@Injectable({
  providedIn: 'root'
})
export class AddEventService {

  constructor( private authService: AuthService,
    @Inject('API_URL') private api) { }

    public addJamEvent(jamEventInfo: JamEventInfo): Promise<any> {
      console.log(jamEventInfo);
      return this.authService.post(this.api + '/JamEvent/AddJamEvent', jamEventInfo, 'application/json; charset=utf-8');
    }
}
