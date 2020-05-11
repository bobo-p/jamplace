import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { JamUserModel } from '../jam-user-info';

@Injectable({
  providedIn: 'root'
})
export class LoggedUserService {

  private loggedUser: JamUserModel;
  constructor(
    private authService: AuthService,
    @Inject('API_URL')
    private api
  ) { }
  private getJamUserData(): Promise<JamUserModel> {
    return this.authService.get(this.api + '/JamUser/GetLoggedUserInfo');
  }
  tryGetLoggedUser() : Promise<boolean> {

    return this.getJamUserData().then(data => {
      this.loggedUser = data;
      return true;
    }, fail => {    
      console.log(fail);
      return false;
    });
  }
  public getCurrentLoggedUser() : JamUserModel {
    if(!this.loggedUser)
      this.tryGetLoggedUser();
    return this.loggedUser;
  }
  public updateUserData()
  {
    this.tryGetLoggedUser();
  }
}
