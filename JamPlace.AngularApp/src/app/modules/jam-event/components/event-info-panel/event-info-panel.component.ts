import { Component, OnInit, ElementRef, ViewChild, Input, OnDestroy } from '@angular/core';
import * as M from "materialize-css/dist/js/materialize";
import { JamEventInfo } from '../../../shared/jam-event-info';
import { MatDialog } from '@angular/material';
import { EditJamEventDialogComponent } from 'src/app/modules/shared/components/edit-jam-event-dialog/edit-jam-event-dialog.component';
import { Address } from 'src/app/modules/shared/addres';

@Component({
  selector: 'app-event-info-panel',
  templateUrl: './event-info-panel.component.html',
  styleUrls: ['./event-info-panel.component.scss']
})
 
export class EventInfoPanelComponent implements OnInit, OnDestroy {

  @Input('event-info') eventInfo: JamEventInfo;
  @ViewChild('collapsible',{static: false}) elCollapsible: ElementRef;
  private addressText: string;

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
    this.addressText = this.prepareAddrressString(this.eventInfo.address);
  }

  ngAfterViewInit() {
    let instanceCollapsible = new M.Collapsible(this.elCollapsible.nativeElement, {});
    
  }

  ngOnDestroy() {
    
  }
  openDialog(model: JamEventInfo): void {  
    const dialogRef = this.dialog.open(EditJamEventDialogComponent, {
      width: '550px',
      data: model
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.eventInfo = result;
        this.addressText = this.prepareAddrressString(this.eventInfo.address);
      }
    });
  }
  private prepareAddrressString(addres: Address): string {
    var text='';
    if(!addres) return text;
    if(addres.street)
      text+=addres.street;
    if(addres.street && addres.localNumber)
      text+=" "+addres.localNumber;
    if(addres.city) {
      if(text === '')
        text+=addres.city;
      else
      text+=", "+addres.city;
    }
    if(addres.country) {
      if(text === '')
        text+=addres.country;
      else
        text+=", "+addres.country;
    }
    return text;
  }

}
