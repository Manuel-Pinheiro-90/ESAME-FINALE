import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { iEvento } from '../../interface/ievento';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss',
})
export class EventsComponent implements OnInit {
  eventi: iEvento[] = [];

  constructor(private eventService: EventService) {}
  ngOnInit(): void {
    this.eventService.getEvents().subscribe(
      (data) => {
        this.eventi = data;
      },
      (error) => {
        console.error('error', error);
      }
    );
  }

  deleteEvent(id: number): void {
    if (confirm('sei sicuro di coler cancellare questo evento?')) {
      this.eventService.deleteEvent(id).subscribe(
        () => {
          this.eventi = this.eventi.filter((e) => e.id !== id);
        },
        (error) => {
          console.error('error', error);
        }
      );
    }
  }
}
