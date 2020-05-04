import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { CustomSearchFieldComponent } from './components/custom-search-field/custom-search-field.component';
import { CommonModule } from '@angular/common';  
import { FormsModule } from '@angular/forms';

const components = [
    ConfirmDialogComponent,
    CustomSearchFieldComponent
];


@NgModule({
  declarations: [
    ...components
  ],
  entryComponents: [ConfirmDialogComponent],
  imports: [
    MaterialModule,
    CommonModule,
    FormsModule
  ],

 exports: [
     ...components,
     MaterialModule
 ]
})
export class SharedModule { }
