import { Component, OnInit, Input } from '@angular/core';
import { EquipmentViewModel } from '../../models/equipment-vewmodel';
import { BehaviorSubject } from 'rxjs';
import { EqupimentSearchRequest } from '../../models/equpiment-search-request';
import { MatDialog } from '@angular/material';
import { EquipmentService } from '../../services/equipment.service';
import { ConfirmDialogComponent } from 'src/app/modules/shared/components/confirm-dialog/confirm-dialog.component';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { AddProvidedEquipmentDialogComponent } from './add-provided-equipment-dialog/add-provided-equipment-dialog.component';

@Component({
  selector: 'app-provided-equipment-list',
  templateUrl: './provided-equipment-list.component.html',
  styleUrls: ['./provided-equipment-list.component.scss']
})
export class ProvidedEquipmentListComponent implements OnInit {

  @Input('event-id') eventId: number;
  @Input('eq-list') equipmentList: EquipmentViewModel[];

   equipment$: BehaviorSubject<EquipmentViewModel[]>;
   private searchTerms: BehaviorSubject <string>;
   firstSearch: boolean;
   listEmpty: boolean;
   searchRequest: EqupimentSearchRequest;

  constructor(public dialog: MatDialog,
    private equipmentService: EquipmentService) { }

  ngOnInit() {
    this.firstSearch = true;
    this.equipment$ = new BehaviorSubject<EquipmentViewModel[]>(this.equipmentList);
    if(!this.equipmentList || this.equipmentList.length === 0) {
      this.listEmpty=true;
    }
  }

  openDialog(model?: EquipmentViewModel): void {
    if(!model)
      model = new EquipmentViewModel();
    model.eventId = this.eventId;
    const dialogRef = this.dialog.open(AddProvidedEquipmentDialogComponent, {
      width: '450px',
      data: model
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.listEmpty=false;
        this.updateEquipmentList(result);
        this.equipment$.next(this.equipmentList);
      }
    });
  }
  deleteEquipment(equipment: EquipmentViewModel): void
  {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: {
          title: "Usuwanie ekqipunky",
          message: "Potwierdź usunięcie wybranego przedmiotu"}
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
     if(dialogResult)
     {
        this.equipmentService.deleteProvidedEquipment(equipment).then(response => {   
          this.equipmentList = this.equipmentList.filter(item => item !== equipment);
          if(this.equipmentList.length === 0)
            this.listEmpty=true;
          this.equipment$.next(this.equipmentList);
          },
           error => {
            console.log(error);
        }); 
     }      
   });
  }
  search(term: string): void {

    if(this.firstSearch)
    {     
      this.searchRequest = new EqupimentSearchRequest(term, this.eventId);
      this.searchTerms = new BehaviorSubject<string>(term);
      var obs = this.searchTerms.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.equipmentService.searchProvidedEquipment(this.searchRequest))
      );
      obs.subscribe(items => {
        this.equipmentList = items;
        if(this.equipmentList.length === 0)
          this.listEmpty=true;
        else
          this.listEmpty=false;
        this.equipment$.next(this.equipmentList);
      });
      this.firstSearch = false;
    }
    else {
      this.searchRequest.searchText = term;
      this.searchTerms.next(term);
    }   
  }
  private updateEquipmentList(newItem){

    let updateItem = this.equipmentList.find(this.findIndexToUpdate, newItem.id);
    if(!updateItem)
    {
      this.equipmentList.unshift(newItem);
      return;
    }
    let index = this.equipmentList.indexOf(updateItem);
    this.equipmentList[index] = newItem;

  }
  private findIndexToUpdate(newItem) { 
    return newItem.id === this;
}

}
