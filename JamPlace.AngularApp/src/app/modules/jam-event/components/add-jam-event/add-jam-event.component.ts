import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormControl,Validators  } from '@angular/forms';
import { trigger, state, style, transition, animate } from '@angular/animations';
import {JamEventInfo} from '../../../shared/jam-event-info'
import {JamEventService} from '../../services/jam-event.service'
import { Router, ActivatedRoute } from '@angular/router';
import * as M from "materialize-css/dist/js/materialize";

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

   datePicker;
   timePicker;
  constructor(
    private addEventService: JamEventService,
    private router: Router, private r:ActivatedRoute ) {
    this.addJamEventForm=this.createFormGroup();       
   }

  ngOnInit() {
  }

  ngAfterViewInit() {
    var options = {
      //format : 'dd.mm.yyyy',
      setDefaultDate: true,
    };  
    var elems = document.querySelectorAll('.datepicker');
    var datapickers = M.Datepicker.init(elems, options);
    this.datePicker = datapickers[0];
    var today = new Date();
    this.datePicker.setDate(today);


    var pickTime = this.getFormattedTime(today);
    var timeOptions = {
      twelveHour: false,
      defaultTime: pickTime
    }; 
    var timeElems = document.querySelectorAll('.timepicker');
    var timepickers = M.Timepicker.init(timeElems, timeOptions);
    this.timePicker = timepickers[0];
    
    this.timePicker.time = pickTime;  

  }

  onSubmit() {
    var date = new Date(this.datePicker.toString());
    this.submitted=true;
      if (this.addJamEventForm.invalid ) {
        return;
    }
    const result: JamEventInfo = Object.assign({}, this.addJamEventForm.value);
    result.address = Object.assign({}, result.address);

    var tempDate=new Date(this.datePicker.toString());
    var timeDate=this.timePicker.time.split(":");
    result.date = new Date(tempDate.getFullYear(),tempDate.getMonth(),tempDate.getDate(),timeDate[0],timeDate[1]); 
    this.addEventService.addJamEvent(result).then(resultId => {
      
      this.router.navigate(['main-event',resultId],{ relativeTo: this.r.parent });
      },
       error => {
        console.log(error);
        this.submitted=false;
    });

  }
  createFormGroup() {
    var date = new Date();
    var time= date.getHours()  + ":" + date.getMinutes();
    return new FormGroup({
      name: new FormControl('',[Validators.required]),
      date: new FormControl(date),
      time: new FormControl(time),
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

  private getFormattedTime(date: Date): string {
    
    var tempTime=new Date(date); 
    var hours= tempTime.getHours().toString() ;
    var minutes =tempTime.getMinutes().toString();
    if(hours.length === 1) {
      hours = "0" + hours;   
    }
      
    if(minutes.length === 1)
      minutes = "0" + minutes;
    var res= hours +":"+ minutes;
    return res;
  }
}
