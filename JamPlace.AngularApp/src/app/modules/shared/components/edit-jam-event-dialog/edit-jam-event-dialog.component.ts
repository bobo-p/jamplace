import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { JamEventInfo } from '../../jam-event-info';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Address } from '../../addres';
import { JamEventInfoService } from '../../services/jam-event-info.service';
import * as M from "materialize-css/dist/js/materialize";

@Component({
  selector: 'app-edit-jam-event-dialog',
  templateUrl: './edit-jam-event-dialog.component.html',
  styleUrls: ['./edit-jam-event-dialog.component.scss']
})
export class EditJamEventDialogComponent implements OnInit {

  editJamEventForm: FormGroup;
  submitted = false;
   datePicker;
   timePicker;
   selectedTime: string;

  constructor(public dialogRef: MatDialogRef<EditJamEventDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public jamEventInfo: JamEventInfo,
    private jamEventInfoService: JamEventInfoService
    ) {   
      if(!this.jamEventInfo.address)
        this.jamEventInfo.address = new Address();
      this.editJamEventForm=this.createFormGroup();
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
    this.datePicker.setDate(this.jamEventInfo.date);


    var tempDate=new Date(this.jamEventInfo.date);

    var pickTime = this.getFormattedTime(tempDate);
    var timeOptions = {
      twelveHour: false,
      defaultTime: pickTime
    }; 
    var timeElems = document.querySelectorAll('.timepicker');
    var timepickers = M.Timepicker.init(timeElems, timeOptions);
    this.timePicker = timepickers[0];
    
    this.timePicker.time = pickTime;  

  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onSubmit() {

    this.submitted=true;
      if (this.editJamEventForm.invalid ) {
        return;
    }
    const result: JamEventInfo = Object.assign({}, this.editJamEventForm.value);
    result.address = Object.assign({}, result.address);
    result.id = this.jamEventInfo.id;
    var tempDate=new Date(this.datePicker.toString());
    var timeDate=this.timePicker.time.split(":");
    result.date = new Date(tempDate.getFullYear(),tempDate.getMonth(),tempDate.getDate(),timeDate[0],timeDate[1]); 
    this.jamEventInfoService.updateJamEvent(result).then(response => {   
      this.dialogRef.close(result);
      },
      error => {
        console.log(error);
        this.submitted=false;
    });
  }

  createFormGroup() {
    var tempTime=new Date(this.jamEventInfo.date);
    var time= tempTime.getHours()  + ":" + tempTime.getMinutes();
    return new FormGroup({
      name: new FormControl(this.jamEventInfo.name,[Validators.required]),
      date: new FormControl(this.jamEventInfo.date),
      time: new FormControl(time),
      size: new FormControl(this.jamEventInfo.size),
      description: new FormControl(this.jamEventInfo.description),
      address: new FormGroup({
        street: new FormControl(this.jamEventInfo.address.street),
        localNumber: new FormControl(this.jamEventInfo.address.localNumber),
        city: new FormControl(this.jamEventInfo.address.city),
        country: new FormControl(this.jamEventInfo.address.country),
      })
    });
  }
  get name() {
    return this.editJamEventForm.get('name');
  }
  get f() { return this.editJamEventForm.controls; }

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
