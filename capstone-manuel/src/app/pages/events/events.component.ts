import { Component, OnInit, OnDestroy } from '@angular/core';
import { EventService } from '../../services/event.service';
import { iEvento } from '../../interface/ievento';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss',
})
export class EventsComponent implements OnInit, OnDestroy {
  eventi: iEvento[] = [];
  filteredEvents: iEvento[] = [];
  routerSubscription!: Subscription;
  eventsSubscription!: Subscription;
  searchTerm: string = '';
  isAdmin: boolean = false;

  constructor(
    private authService: AuthService,
    private eventService: EventService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.user$.subscribe(
      (user) =>
        (this.isAdmin = user?.ruoli.some((r) => r.nome === 'Admin') || false)
    );

    this.eventsSubscription = this.eventService.event$.subscribe(
      (data) => {
        this.filteredEvents = this.eventi = data;
      },
      (error) => {
        console.error('Errore nel caricamento degli eventi', error);
      }
    );

    this.routerSubscription = this.router.events
      .pipe(
        filter(
          (event) => event instanceof NavigationEnd && event.url === '/events'
        )
      )
      .subscribe(() => this.loadEvents());
  }

  loadEvents(): void {
    this.eventService.getEvents().subscribe(
      (data) => (this.filteredEvents = this.eventi = data),
      (error) => console.error('Errore nel caricamento degli eventi', error)
    );
  }

  deleteEvent(id: number): void {
    if (confirm('Sei sicuro di voler cancellare questo evento?')) {
      this.eventService.deleteEvent(id).subscribe(
        () => {
          this.eventi = this.eventi.filter((e) => e.id !== id);
          this.filteredEvents = this.filteredEvents.filter((e) => e.id !== id);
        },
        (error) => console.error('Error', error)
      );
    }
  }

  filterEvents(): void {
    const term = this.searchTerm.toLowerCase();
    this.filteredEvents = term
      ? this.eventi.filter(
          (evento) =>
            evento.titolo.toLowerCase().includes(term) ||
            evento.luogo.toLowerCase().includes(term)
        )
      : [...this.eventi];
  }

  ngOnDestroy(): void {
    this.routerSubscription?.unsubscribe();
    this.eventsSubscription?.unsubscribe();
  }
}
