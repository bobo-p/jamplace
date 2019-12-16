import { Injectable,Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
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
}
