import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { SongViewModel } from 'src/app/modules/jam-event/models/song-vewmodel';
import { FormGroup, FormControl, Validators  } from '@angular/forms';
import { SongService } from 'src/app/modules/jam-event/services/song.service';

@Component({
  selector: 'app-add-song-dialog',
  templateUrl: './add-song-dialog.component.html',
  styleUrls: ['./add-song-dialog.component.scss']
})
export class AddSongDialogComponent implements OnInit {

   addSongForm: FormGroup;
   submitted = false;
   currentSong: SongViewModel;
   isEditingMode: boolean;
  constructor(
    public dialogRef: MatDialogRef<AddSongDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public song: SongViewModel,
    private songService: SongService) {
     this.currentSong = song;
     this.addSongForm = this.createFormGroup();
     if(song.id)
      this.isEditingMode = true;
    }

  onNoClick(): void {
    this.dialogRef.close();
  }
  ngOnInit(): void {

  }
  onSubmit() {
    this.submitted=true;
      if (this.addSongForm.invalid ) {
        return;
    }
    var result: SongViewModel = Object.assign({}, this.addSongForm.value);
    result.eventId = this.currentSong.eventId;
    if(!this.isEditingMode) {
      this.songService.addSong(result).then(response => {   
        result = response;
        this.dialogRef.close(result);
        },
        error => {
          console.log(error);
          this.submitted=false;
      });
      return;
    }
    result.id = this.currentSong.id;
    this.songService.updateSong(result).then(response => {   
      this.dialogRef.close(result);
      },
      error => {
        console.log(error);
        this.submitted=false;
    });
  }
  createFormGroup() {
    if(!this.currentSong.id) {
      return new FormGroup({
        title: new FormControl('',[Validators.required]),
        artist: new FormControl(),
        description: new FormControl(),
        link: new FormControl(),      
      });
    }
    var formGroup = new FormGroup({
      title: new FormControl(this.currentSong.title,[Validators.required]),
      artist: new FormControl(this.currentSong.artist),
      description: new FormControl(this.currentSong.description),
      link: new FormControl(this.currentSong.link),       
    });

    return formGroup;
  }
  get title() {
    return this.addSongForm.get('title');
  }
  get f() { return this.addSongForm.controls; }
}
