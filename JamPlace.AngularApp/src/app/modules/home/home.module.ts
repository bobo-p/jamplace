import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyEventsListComponent } from './components/my-events-list/my-events-list.component';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home.routing';




@NgModule({
  declarations: [HomeComponent, MyEventsListComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
  ]
})
export class HomeModule { }
