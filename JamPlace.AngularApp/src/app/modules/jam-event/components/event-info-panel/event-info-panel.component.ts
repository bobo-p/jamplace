import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import * as M from "materialize-css/dist/js/materialize";



@Component({
  selector: 'app-event-info-panel',
  templateUrl: './event-info-panel.component.html',
  styleUrls: ['./event-info-panel.component.scss']
})
 

export class EventInfoPanelComponent implements OnInit {

  @ViewChild('collapsible',{static: false}) elCollapsible: ElementRef;
  
  constructor() { }

  ngOnInit() {
   
  }

  ngAfterViewInit() {
    let instanceCollapsible = new M.Collapsible(this.elCollapsible.nativeElement, {});
}

}
