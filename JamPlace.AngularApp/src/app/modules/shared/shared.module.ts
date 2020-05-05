import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { CustomSearchFieldComponent } from './components/custom-search-field/custom-search-field.component';
import { CommonModule } from '@angular/common';  
import { FormsModule } from '@angular/forms';
import { EditJamEventDialogComponent } from './components/edit-jam-event-dialog/edit-jam-event-dialog.component';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IgxTimePickerModule } from 'igniteui-angular';

const components = [
    ConfirmDialogComponent,
    CustomSearchFieldComponent
];


@NgModule({
  declarations: [
    ...components,
    EditJamEventDialogComponent
  ],
  entryComponents: [ConfirmDialogComponent, EditJamEventDialogComponent],
  imports: [
    MaterialModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],

 exports: [
     ...components
 ]
})
export class SharedModule { }
