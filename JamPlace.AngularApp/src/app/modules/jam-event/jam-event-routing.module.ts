import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {JamEventRouterComponent} from './jam-event-router.component';
import {AddJamEventComponent} from './components/add-jam-event/add-jam-event.component'; 
import {MainEventPanelComponent} from './components/main-event-panel/main-event-panel.component'; 


const routes: Routes = [
  {
    path: '',
    children: [
      
      { path: 'addJamEvent', component: AddJamEventComponent },
      { path: 'main-event/:id', component: MainEventPanelComponent },
      { path: '**', redirectTo: '/'}
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class JamEventRoutingModule { }