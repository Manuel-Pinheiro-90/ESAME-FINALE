import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { iEvento } from '../../interface/ievento';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss',
})
export class EventsComponent implements OnInit {
  eventi: iEvento[] = [];
  filteredEvents: iEvento[] = []; //
  routerSubscription!: Subscription;
  searchTerm: string = ''; //

  constructor(private eventService: EventService, private router: Router) {}
  ngOnInit(): void {
    this.loadEvents();
    this.routerSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/events') {
        this.loadEvents(); // Ricarica gli eventi quando torni sulla pagina degli eventi
      }
    });
  }

  loadEvents(): void {
    this.eventService.getEvents().subscribe(
      (data) => {
        this.eventi = data;
        this.filteredEvents = data; //
      },
      (error) => {
        console.error('Errore nel caricamento degli eventi', error);
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

  filterEvents(): void {
    if (this.searchTerm) {
      this.filteredEvents = this.eventi.filter((evento) =>
        evento.titolo
          .toLowerCase()
          .includes(this.searchTerm.toLocaleLowerCase())
      ); //
    } else {
      this.filteredEvents = [...this.eventi];
    }
  }

  ngOnDestroy(): void {
    // Unsubscribe per evitare memory leak
    if (this.routerSubscription) {
      this.routerSubscription.unsubscribe();
    }
  }
}
