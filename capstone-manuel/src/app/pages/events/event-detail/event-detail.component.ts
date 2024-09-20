import { Component, OnInit } from '@angular/core';
import { iEvento } from '../../../interface/ievento';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../../services/event.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss',
})
export class EventDetailComponent implements OnInit {
  evento: iEvento | undefined;
  eventId!: number;
  eventSubscription!: Subscription;
  constructor(
    private route: ActivatedRoute,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    this.eventId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.eventId) {
      // Sottoscrivi il BehaviorSubject
      this.eventSubscription = this.eventService.event$.subscribe((events) => {
        // Cerca l'evento nel BehaviorSubject
        this.evento = events.find((e) => e.id === this.eventId);

        // Se l'evento non è presente nel BehaviorSubject, effettua una chiamata HTTP
        if (!this.evento) {
          this.loadEventFromServer();
        }
      });
    }
  }

  // Carica l'evento dal server (nel caso non sia già nel BehaviorSubject)
  loadEventFromServer(): void {
    this.eventService.getEvent(this.eventId).subscribe(
      (data) => {
        this.evento = data;

        this.eventService.updateEventInSubject(data);
      },
      (error) => {
        console.error(
          "Errore durante il caricamento del dettaglio dell'evento:",
          error
        );
      }
    );
  }

  ngOnDestroy(): void {
    // Evita memory leak
    this.eventSubscription?.unsubscribe();
  }
}
