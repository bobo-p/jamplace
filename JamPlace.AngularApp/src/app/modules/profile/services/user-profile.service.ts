import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { Observable } from 'rxjs';
import { JamUserModel } from '../../shared/jam-user-info';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

  public updateUserInfo(request: JamUserModel): Promise<any> {
    return this.authService.post(this.api + '/JamUser/SaveUserData/', request, 'application/json; charset=utf-8');
  }
}
