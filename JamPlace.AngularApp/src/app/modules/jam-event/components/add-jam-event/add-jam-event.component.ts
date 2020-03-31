import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl,Validators  } from '@angular/forms';
import { trigger, state, style, transition, animate } from '@angular/animations';
import {JamEventInfo} from '../../models/jam-event-info'
import {AddEventService} from '../../services/add-event.service'
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
  submitted = false;
  
  constructor(
    private addEventService: AddEventService) {
    this.addJamEventForm=this.createFormGroup();
   }

  ngOnInit() {
  }
  onSubmit() {
    this.submitted = true;
      if (this.addJamEventForm.invalid) {
        return;
    }
    const result: JamEventInfo = Object.assign({}, this.addJamEventForm.value);
    result.address = Object.assign({}, result.address);
    this.addEventService.addJamEvent(result).then(ok => {}, function(data) {
      console.log(data);
  });

  }
  createFormGroup() {
    return new FormGroup({
      name: new FormControl('',[Validators.required]),
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
  get name() {
    return this.addJamEventForm.get('name');
  }
  get f() { return this.addJamEventForm.controls; }

}
