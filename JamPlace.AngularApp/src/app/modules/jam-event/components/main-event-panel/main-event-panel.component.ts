import { Component, OnInit, OnDestroy  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JamEventViewModel } from '../../models/jam-event-viewmodel';
import { JamEventService } from '../../services/jam-event.service';
import { JamEventInfo } from '../../models/jam-event-info';

@Component({
  selector: 'app-main-event-panel',
  templateUrl: './main-event-panel.component.html',
  styleUrls: ['./main-event-panel.component.scss']
})
export class MainEventPanelComponent implements OnInit, OnDestroy {

  private eventId: number;
  private sub: any;
  private jamEvent: JamEventViewModel;
  private jamEventInfo: JamEventInfo;
  
  constructor(
    private route: ActivatedRoute,
    private eventService: JamEventService)  { }

  ngOnInit() {    
    this.sub = this.route.params.subscribe(params => {
       this.eventId = params['id'];
       this.eventService.getJamEevent(this.eventId).then(result => {
        this.jamEvent=result;   
        this.jamEventInfo=this.CreateEventInfo(this.jamEvent);    
        },
         error => {
          console.log(error);
      }); 
    });
  }
  ngOnDestroy() {
    this.sub.unsubscribe();
  }
  private CreateEventInfo(jamEvent: JamEventViewModel): JamEventInfo
  {
    var evInfo: JamEventInfo  =
    {
      name: jamEvent.name,
      description: jamEvent.description,
      address: jamEvent.address,
      size: jamEvent.size,
      date: jamEvent.date
    };
    return evInfo;
  }

}
