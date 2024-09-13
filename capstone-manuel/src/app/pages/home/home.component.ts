import { AfterViewInit, Component, ElementRef, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit, AfterViewInit {
  constructor(private el: ElementRef) {}

  ngOnInit(): void {
    // Puoi inizializzare altre cose se necessario qui
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
