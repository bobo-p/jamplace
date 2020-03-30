import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddJamEventComponent } from './components/add-jam-event/add-jam-event.component';
import { JamEventRouterComponent } from './jam-event-router.component';
import {JamEventRoutingModule} from './jam-event-routing.module'
import { ReactiveFormsModule } from '@angular/forms'

@NgModule({
  declarations: [AddJamEventComponent, JamEventRouterComponent],
  imports: [
    CommonModule,
    JamEventRoutingModule,
    ReactiveFormsModule
  
  ]
})
export class JamEventModule { }
