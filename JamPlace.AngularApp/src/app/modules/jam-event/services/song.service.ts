import { Injectable, Inject } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';
import { SongViewModel } from '../models/song-vewmodel';
import { Observable } from 'rxjs';
import { SongSearchRequest } from '../models/song-search-request';

@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private authService: AuthService,
    @Inject('API_URL')
    private api) { }

    public addSong(song: SongViewModel): Promise<any> {
      return this.authService.post(this.api + '/Song/AddSong', song, 'application/json; charset=utf-8');
    }

    public updateSong(song: SongViewModel): Promise<any> {
      return this.authService.post(this.api + '/Song/UpdateSong', song, 'application/json; charset=utf-8');
    }

    public deleteSong(song: SongViewModel): Promise<any> {
      return this.authService.post(this.api + '/Song/DeleteSong', song, 'application/json; charset=utf-8');
    }

    public getSongs(eventId: number): Promise<SongViewModel> {
      return this.authService.get(this.api + '/Song/GetSongs/' + eventId);
    }

    public searchByName(request: SongSearchRequest): Observable<SongViewModel[]> {
      return this.authService.postAsObservable(this.api + '/Song/SearchSongByName/', request, 'application/json; charset=utf-8');
    }

}
