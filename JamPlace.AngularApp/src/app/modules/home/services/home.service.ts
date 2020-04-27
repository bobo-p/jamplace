import { Injectable,Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { JamUserModel } from '../../shared/jam-user-info';
@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(
    private authService: AuthService,
    @Inject('API_URL') private api
  ) { }

  getTestString(): Promise<string> {
    return this.authService.get(this.api + '/Home/GetTestString');
  }

  getJamUserData(): Promise<JamUserModel> {
    return this.authService.get(this.api + '/JamUser/getUserInfo');
  }
}
