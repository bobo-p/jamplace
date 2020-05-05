import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddJamEventComponent } from './components/add-jam-event/add-jam-event.component';
import { JamEventRouterComponent } from './jam-event-router.component';
import {JamEventRoutingModule} from './jam-event-routing.module'
import { ReactiveFormsModule } from '@angular/forms';
import { MainEventPanelComponent } from './components/main-event-panel/main-event-panel.component';
import { EventInfoPanelComponent } from './components/event-info-panel/event-info-panel.component';
import { ForumComponent } from './components/forum/forum.component';
import { SongListComponent } from './components/song-list/song-list.component';
import { EquipmentListComponent } from './components/equipment-list/equipment-list.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { AddSongDialogComponent } from './components/song-list/add-song-dialog/add-song-dialog/add-song-dialog.component'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule  } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';

@NgModule({
  declarations: [AddJamEventComponent, JamEventRouterComponent, MainEventPanelComponent, EventInfoPanelComponent, ForumComponent, SongListComponent, EquipmentListComponent, UserListComponent, AddSongDialogComponent],
  imports: [
    CommonModule,
    JamEventRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule, 
    SharedModule
  ],
  entryComponents: [
    AddSongDialogComponent
 ]
})
export class JamEventModule { }
