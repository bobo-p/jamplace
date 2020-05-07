import { Component, OnInit, OnDestroy  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JamEventViewModel } from '../../models/jam-event-viewmodel';
import { JamEventService } from '../../services/jam-event.service';
import { JamEventInfo } from '../../../shared/jam-event-info';

@Component({
  selector: 'app-main-event-panel',
  templateUrl: './main-event-panel.component.html',
  styleUrls: ['./main-event-panel.component.scss']
})
export class MainEventPanelComponent implements OnInit, OnDestroy {

   eventId: number;
   sub: any;
   jamEvent: JamEventViewModel;
   jamEventInfo: JamEventInfo;
  
  constructor(
    private route: ActivatedRoute,
    private eventService: JamEventService)  { }

  ngOnInit() {    
    this.sub = this.route.params.subscribe(params => {
       this.eventId = params['id'];
       this.eventService.getJamEevent(this.eventId).then(result => {
        console.log(result);
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
      id: jamEvent.id,
      name: jamEvent.name,
      description: jamEvent.description,
      address: jamEvent.address,
      size: jamEvent.size,
      date: jamEvent.date
    };
    return evInfo;
  }

}
