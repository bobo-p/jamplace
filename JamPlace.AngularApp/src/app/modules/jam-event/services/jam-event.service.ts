import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { JamEventInfo } from '../../shared/jam-event-info';
import { JamEventViewModel } from '../models/jam-event-viewmodel';
import { Observable } from 'rxjs';
import { JamEventSearchRequest } from '../models/jam-event-search-request';
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
  public getJamEevents(): Promise<JamEventViewModel[]> {
    return this.authService.get(this.api + '/JamEvent/GetEvents/');
  }
  public search(request: JamEventSearchRequest): Observable<JamEventViewModel[]> {
    return this.authService.postAsObservable(this.api + '/JamEvent/Search/', request, 'application/json; charset=utf-8');
  }
  public joinJamEvent(eventId: number): Promise<any> {
    console.log(eventId);
    return this.authService.get(this.api + '/JamEvent/JoinJamEvent/'+ eventId);
  }
  public deleteJamEevent(eventId: number): any {
    return this.authService.delete(this.api + '/JamEvent/DeleteEvent/' + eventId);
  }
  public leaveJamEevent(eventId: number): any {
    return this.authService.delete(this.api + '/JamEvent/LeaveEvent/' + eventId);
  }
}
