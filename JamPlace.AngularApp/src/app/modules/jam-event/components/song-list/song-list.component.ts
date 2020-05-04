import { Component, OnInit, Input } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';
import {MatDialog} from '@angular/material/dialog';
import { SongViewModel } from '../../models/song-vewmodel';
import { AddSongDialogComponent } from './add-song-dialog/add-song-dialog/add-song-dialog.component';
import { ConfirmDialogComponent } from 'src/app/modules/shared/components/confirm-dialog/confirm-dialog.component';
import { SongService } from '../../services/song.service';
import { SongSearchRequest } from '../../models/song-search-request';


@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.scss']
})
export class SongListComponent implements OnInit {

  @Input('event-id') eventId: number;
  @Input('song-list') songList: SongViewModel[];

  private songs$: BehaviorSubject<SongViewModel[]>;
  private searchTerms: BehaviorSubject <string>;
  private firstSearch: boolean;
  private listEmpty: boolean;
  private searchRequest: SongSearchRequest;

  constructor(public dialog: MatDialog,
    private songService: SongService
    ) { }

  ngOnInit() { 
    this.firstSearch = true;
    this.indexSongList();
    this.songs$ = new BehaviorSubject<SongViewModel[]>(this.songList);
    if(!this.songList || this.songList.length === 0) {
      this.listEmpty=true;
      this.songList = new SongViewModel[0];
    }
  }

  openDialog(model: SongViewModel): void {
    if(!model)
      model = new SongViewModel();
      
    model.eventId = this.eventId;
    const dialogRef = this.dialog.open(AddSongDialogComponent, {
      width: '450px',
      data: model
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.listEmpty=false;
        this.updateSongList(result);
        console.log(this.songList);
        this.indexSongList();
       
        this.songs$.next(this.songList);
      }
    });
  }

  deleteSong(song: SongViewModel): void
  {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Usuwanie piosenki",
          message: "Chesz usunąć: \"" + song.title + "\" ?"}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
     if(dialogResult)
     {
        this.songService.deleteSong(song).then(response => {   
          this.songList = this.songList.filter(item => item !== song);
          if(this.songList.length === 0)
            this.listEmpty=true;
          this.indexSongList();
          this.songs$.next(this.songList);
          },
           error => {
            console.log(error);
        }); 
     }      
   });
  }

  search(term: string): void {

    if(this.firstSearch)
    {     
      this.searchRequest = new SongSearchRequest(term, this.eventId);
      this.searchTerms = new BehaviorSubject<string>(term);
      var obs = this.searchTerms.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.songService.searchByName(this.searchRequest))
      );
      obs.subscribe(items => {
        this.songList = items;
        if(this.songList.length === 0)
            this.listEmpty=true;
        this. indexSongList();
        this.songs$.next(this.songList);
      });
      this.firstSearch = false;
    }
    else {
      this.searchRequest.searchText = term;
      this.searchTerms.next(term);
    }   
  }

  private indexSongList(): void
  {
    this.songList.forEach( (song, index) => {
      song.index=index+1;
    });   
  }

  private updateSongList(newItem){

    let updateItem = this.songList.find(this.findIndexToUpdate, newItem.id);
    if(!updateItem)
    {
      this.songList.unshift(updateItem);
      return;
    }
    let index = this.songList.indexOf(updateItem);
    this.songList[index] = newItem;

  }

  private findIndexToUpdate(newItem) { 
        return newItem.id === this;
  }
}
