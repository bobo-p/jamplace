import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { JamEventInfo } from '../../jam-event-info';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Address } from '../../addres';
import { JamEventInfoService } from '../../services/jam-event-info.service';

@Component({
  selector: 'app-edit-jam-event-dialog',
  templateUrl: './edit-jam-event-dialog.component.html',
  styleUrls: ['./edit-jam-event-dialog.component.scss']
})
export class EditJamEventDialogComponent implements OnInit {

  editJamEventForm: FormGroup;
  submitted = false;

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
    this.jamEventInfoService.updateJamEvent(result).then(response => {   
      this.dialogRef.close(result);
      },
      error => {
        console.log(error);
        this.submitted=false;
    });
  }

  createFormGroup() {
    return new FormGroup({
      name: new FormControl(this.jamEventInfo.name,[Validators.required]),
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

}
