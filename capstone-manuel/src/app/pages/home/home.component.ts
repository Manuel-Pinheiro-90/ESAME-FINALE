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

  text1: string =
    'Crea il tuo personaggio e rendilo indimenticabile! Su LARP HUB hai la possibilità di dare vita a un eroe unico. Ogni dettaglio che aggiungi al tuo personaggio contribuisce a raccontare una storia originale che riflette la tua creatività. Condividi le tue creazioni con la community e lascia che il tuo personaggio ispiri gli altri giocatori nelle loro avventure. Che tu sia un abile guerriero, un saggio mago o un astuto ladro, il mondo di gioco è pronto ad accoglierti e a crescere insieme a te!';
  text2: string =
    'Partecipa a numerosi eventi LARP dal vivo, progettati per tutti i gusti e stili di gioco! Su LARP HUB troverai una vasta selezione di eventi organizzati da associazioni di tutta Italia, dalle epiche battaglie fantasy alle intricate trame horror, fino ai racconti di fantascienza e storie post-apocalittiche. Che tu sia un veterano o un novizio, c’è un evento adatto a te!';
  text3: string =
    "Partecipa agli Eventi Speciali organizzati da LARP HUB e vivi avventure ancora più emozionanti! Questi eventi esclusivi non solo ti metteranno alla prova con sfide uniche e scenari avvincenti, ma ti daranno anche l'opportunità di vincere fantastici premi. Dai costumi e accessori LARP di alta qualità a equipaggiamenti personalizzati per i tuoi personaggi, ogni evento speciale offrirà ricompense eccezionali per i partecipanti più coraggiosi. Preparati a competere, distinguerti sul campo e ottenere oggetti unici che arricchiranno la tua esperienza LARP. Non perdere l’occasione di lasciare il segno e di portare a casa premi epici!";

  showMore1: boolean = false;
  showMore2: boolean = false;
  showMore3: boolean = false;

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
    const scrollElements =
      this.el.nativeElement.querySelectorAll('.scroll-animation');
    const observerOptions = {
      threshold: 0.4, // L'elemento deve essere visibile almeno per il 50%
    };

    const observer = new IntersectionObserver((entries, observer) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('show');
          observer.unobserve(entry.target);
        }
      });
    }, observerOptions);

    // Attiva l'osservatore solo sugli elementi con la classe .fade-in
    fadeInElements.forEach((element: any) => {
      observer.observe(element);
    });
    scrollElements.forEach((element: any) => {
      observer.observe(element);
    });
  }

  toggleText(section: number): void {
    if (section === 1) {
      this.showMore1 = !this.showMore1;
    } else if (section === 2) {
      this.showMore2 = !this.showMore2;
    } else if (section === 3) {
      this.showMore3 = !this.showMore3;
    }
  }
}
