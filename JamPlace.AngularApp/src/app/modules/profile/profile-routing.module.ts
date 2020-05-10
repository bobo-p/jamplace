import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import { ProfileComponent } from './components/profile/profile.component';


const routes: Routes = [
  {
    path: '', component: ProfileComponent,
    children: [
      
      { path: 'profile', component: ProfileComponent },
      
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
export class ProfileRoutingModule { }
