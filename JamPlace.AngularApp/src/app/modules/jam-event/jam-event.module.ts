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
import { UserListComponent } from './components/user-list/user-list.component'

@NgModule({
  declarations: [AddJamEventComponent, JamEventRouterComponent, MainEventPanelComponent, EventInfoPanelComponent, ForumComponent, SongListComponent, EquipmentListComponent, UserListComponent],
  imports: [
    CommonModule,
    JamEventRoutingModule,
    ReactiveFormsModule
  
  ]
})
export class JamEventModule { }
