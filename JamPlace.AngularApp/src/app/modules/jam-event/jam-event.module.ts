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
import { CommentDialogComponent } from './components/forum/comment-dialog/comment-dialog.component';
import { SearchEventComponent } from './components/search-event/search-event.component';
import { AddEquipmentComponent } from './components/equipment-list/add-equipment/add-equipment.component';
import { ProvidedEquipmentListComponent } from './components/provided-equipment-list/provided-equipment-list.component';
import { AddProvidedEquipmentDialogComponent } from './components/provided-equipment-list/add-provided-equipment-dialog/add-provided-equipment-dialog.component';

@NgModule({
  declarations: [AddJamEventComponent, JamEventRouterComponent, MainEventPanelComponent, EventInfoPanelComponent, ForumComponent, SongListComponent, EquipmentListComponent, UserListComponent, AddSongDialogComponent, CommentDialogComponent, SearchEventComponent, AddEquipmentComponent, ProvidedEquipmentListComponent, AddProvidedEquipmentDialogComponent],
  imports: [
    CommonModule,
    JamEventRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule, 
    SharedModule
  ],
  entryComponents: [
    AddSongDialogComponent,CommentDialogComponent,AddEquipmentComponent, AddProvidedEquipmentDialogComponent
 ]
})
export class JamEventModule { }
