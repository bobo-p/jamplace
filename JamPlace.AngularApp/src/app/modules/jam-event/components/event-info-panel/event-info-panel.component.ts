import { Component, OnInit, ElementRef, ViewChild, Input, OnDestroy } from '@angular/core';
import * as M from "materialize-css/dist/js/materialize";
import { JamEventInfo } from '../../../shared/jam-event-info';

@Component({
  selector: 'app-event-info-panel',
  templateUrl: './event-info-panel.component.html',
  styleUrls: ['./event-info-panel.component.scss']
})
 
export class EventInfoPanelComponent implements OnInit, OnDestroy {

  @Input('event-info') eventInfo: JamEventInfo;
  @ViewChild('collapsible',{static: false}) elCollapsible: ElementRef;

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    let instanceCollapsible = new M.Collapsible(this.elCollapsible.nativeElement, {});
  }

  ngOnDestroy() {
    
  }
}
