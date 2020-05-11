import { Injectable, Inject } from '@angular/core';
import { EqupimentSearchRequest } from '../models/equpiment-search-request';
import { AuthService } from '../../auth/services/auth.service';
import { EquipmentViewModel } from '../models/equipment-vewmodel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

  public addNeededEquipment(comment: EquipmentViewModel): Promise<any> {
    return this.authService.post(this.api + '/NeededEquipment/AddEquipment', comment, 'application/json; charset=utf-8');
  }
  public updateNeededEquipment(comment: EquipmentViewModel): Promise<any> {
    return this.authService.post(this.api + '/NeededEquipment/UpdateEquipment', comment, 'application/json; charset=utf-8');
  }
  public deleteNeededEquipment(comment: EquipmentViewModel): Promise<any> {
    return this.authService.post(this.api + '/NeededEquipment/DeleteEquipment', comment, 'application/json; charset=utf-8');
  }
  public searchNeededEquipment(request: EqupimentSearchRequest): Observable<EquipmentViewModel[]> {
    return this.authService.postAsObservable(this.api + '/NeededEquipment/SearchEquipment/', request, 'application/json; charset=utf-8');
  }
  public addProvidedEquipment(comment: EquipmentViewModel): Promise<any> {
    return this.authService.post(this.api + '/EventEquipment/AddEquipment', comment, 'application/json; charset=utf-8');
  }
  public updateProvidedEquipment(comment: EquipmentViewModel): Promise<any> {
    return this.authService.post(this.api + '/EventEquipment/UpdateEquipment', comment, 'application/json; charset=utf-8');
  }
  public deleteProvidedEquipment(comment: EquipmentViewModel): Promise<any> {
    return this.authService.post(this.api + '/EventEquipment/DeleteEquipment', comment, 'application/json; charset=utf-8');
  }
  public searchProvidedEquipment(request: EqupimentSearchRequest): Observable<EquipmentViewModel[]> {
    return this.authService.postAsObservable(this.api + '/EventEquipment/SearchEquipment/', request, 'application/json; charset=utf-8');
  }
}
