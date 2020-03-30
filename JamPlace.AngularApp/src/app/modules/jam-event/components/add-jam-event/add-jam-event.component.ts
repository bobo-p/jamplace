import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { trigger, state, style, transition, animate } from '@angular/animations';
@Component({
  selector: 'app-add-jam-event',
  templateUrl: './add-jam-event.component.html',
  styleUrls: ['./add-jam-event.component.scss'],
  animations: [
    trigger('flyInOut', [
      transition('void => *',[
        style({transform: 'translateX(-100%)'}),
        animate('0.2s')
      ])
    ])
  ]
})
export class AddJamEventComponent implements OnInit {
  addJamEventForm: FormGroup;
  constructor() {
    this.addJamEventForm=this.createFormGroup();
   }

  ngOnInit() {
  }
  onSubmit() {
  }
  createFormGroup() {
    return new FormGroup({
      name: new FormControl(),
      size: new FormControl(),
      description: new FormControl(),
      address: new FormGroup({
        street: new FormControl(),
        localNumber: new FormControl(),
        city: new FormControl(),
        country: new FormControl(),
      })
    });
  }

}
