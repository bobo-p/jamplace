import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EquipmentViewModel } from '../../../models/equipment-vewmodel';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProvidedEquipmentListComponent } from '../provided-equipment-list.component';
import { EquipmentService } from '../../../services/equipment.service';

@Component({
  selector: 'app-add-provided-equipment-dialog',
  templateUrl: './add-provided-equipment-dialog.component.html',
  styleUrls: ['./add-provided-equipment-dialog.component.scss']
})
export class AddProvidedEquipmentDialogComponent implements OnInit {

   eqForm: FormGroup;
   submitted = false;
   currentEq: EquipmentViewModel;
   isEditingMode: boolean;
  constructor(
    public dialogRef: MatDialogRef<ProvidedEquipmentListComponent>,
    @Inject(MAT_DIALOG_DATA) public equipment: EquipmentViewModel,
    private neededEquipmentService: EquipmentService) {
     this.currentEq = equipment;
     this.eqForm = this.createFormGroup();
     if(equipment.id)
      this.isEditingMode = true;
    }

    onNoClick(): void {
      this.dialogRef.close();
    }
    ngOnInit(): void {
  
    }
    onSubmit() {
      this.submitted=true;
        if (this.eqForm.invalid ) {
          return;
      }
      var result = this.currentEq;
      result.name =  this.eqForm.value.name;
      result.eventId = this.currentEq.eventId;
      if(!this.isEditingMode) {
        this.neededEquipmentService.addProvidedEquipment(result).then(response => {   
          result = response;
          this.dialogRef.close(result);
          },
          error => {
            console.log(error);
            this.submitted=false;
        });
        return;
      }
      result.id = this.currentEq.id;
      this.neededEquipmentService.updateProvidedEquipment(result).then(response => {   
        this.dialogRef.close(result);
        },
        error => {
          console.log(error);
          this.submitted=false;
      });
    }

  private createFormGroup() {
    if(!this.currentEq.id) {
      return new FormGroup({
        name: new FormControl('',[Validators.required]),
      
      });
    }
    var formGroup = new FormGroup({
      name: new FormControl(this.currentEq.name,[Validators.required]),         
    });

    return formGroup;
  }
  get name() {
    return this.eqForm.get('name');
  }
  get f() { return this.eqForm.controls; }

}
