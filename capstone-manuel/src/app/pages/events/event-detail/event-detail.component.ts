import { Component, OnInit } from '@angular/core';
import { iEvento } from '../../../interface/ievento';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../../services/event.service';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss',
})
export class EventDetailComponent implements OnInit {
  evento: iEvento | undefined;

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    const eventId = Number(this.route.snapshot.paramMap.get('id'));
    if (eventId) {
      this.eventService.getEvent(eventId).subscribe(
        (data) => {
          this.evento = data;
        },
        (error) => {
          console.error('errore:', error);
        }
      );
    }
  }
}
