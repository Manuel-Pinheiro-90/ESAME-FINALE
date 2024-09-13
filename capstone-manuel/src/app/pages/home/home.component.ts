import { AfterViewInit, Component, ElementRef, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { iEvento } from '../../interface/ievento';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit, AfterViewInit {
  eventi: iEvento[] = [];
  currentIndex: number = 0;
  autoSlideInterval: any;
  constructor(private el: ElementRef, private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents();
  }
  loadEvents(): void {
    this.eventService.getEvents().subscribe(
      (data) => {
        this.eventi = data;
        if (this.eventi.length > 0) {
          this.currentIndex = 0; // Imposta l'evento corrente al primo evento
          this.startAutoSlide();
        }
      },
      (error) => {
        console.error('Errore nel caricamento degli eventi', error);
      }
    );
  }

  startAutoSlide(): void {
    this.autoSlideInterval = setInterval(() => {
      this.nextEvent();
    }, 5000);
  }

  prevEvent(): void {
    this.currentIndex =
      (this.currentIndex > 0 ? this.currentIndex : this.eventi.length) - 1;
  }

  nextEvent(): void {
    this.currentIndex = (this.currentIndex + 1) % this.eventi.length;
  }

  ngAfterViewInit(): void {
    // Seleziona solo gli elementi con la classe .fade-in
    const fadeInElements = this.el.nativeElement.querySelectorAll('.fade-in');

    const observerOptions = {
      threshold: 0.5, // L'elemento deve essere visibile almeno per il 50%
    };

    const observer = new IntersectionObserver((entries, observer) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('show'); // Aggiunge la classe 'show'
          observer.unobserve(entry.target); // Smette di osservare una volta visibile
        }
      });
    }, observerOptions);

    // Attiva l'osservatore solo sugli elementi con la classe .fade-in
    fadeInElements.forEach((element: any) => {
      observer.observe(element);
    });
  }
}
