import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { routing } from './home.routing';


@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    routing
  ]
})
export class HomeModule { }
