import { IPersonaggioDTO } from './../../interface/i-personaggio-dto';
import { Component, OnInit } from '@angular/core';

import { PgService } from '../../services/pg.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IUtenteDTO } from '../../interface/iutente-dto';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-characters-list',
  templateUrl: './characters-list.component.html',
  styleUrl: './characters-list.component.scss',
})
export class CharactersListComponent implements OnInit {
  imageGroups: { url: string; alt: string }[][] = [];

  personaggi: IPersonaggioDTO[] = [];
  descriptionVisible: { [key: number]: boolean } = {};
  currentIndex: number = 0;
  autoSlideInterval: any;

  constructor(private pgService: PgService) {}

  ngOnInit(): void {
    this.loadPersonaggi();
    this.loadImages();
  }

  loadPersonaggi(): void {
    this.pgService.getAllPersonaggi().subscribe({
      next: (personaggi: IPersonaggioDTO[]) => {
        this.personaggi = personaggi;
        this.personaggi.forEach((p) => {
          //
          this.descriptionVisible[p.id] = false;
        }); //
      },
      error: (err: HttpErrorResponse) => {
        console.error('Errore nel recuperare i personaggi:', err);
      },
    });
  }
  toggleDescription(personaggioId: number): void {
    this.descriptionVisible[personaggioId] =
      !this.descriptionVisible[personaggioId];
  }

  deletePersonaggio(id: number): void {
    console.log('ID del personaggio da cancellare:', id);
    if (confirm('Sei sicuro di voler eliminare questo personaggio?')) {
      this.pgService.deletePersonaggio(id).subscribe({
        next: () => {
          this.personaggi = this.personaggi.filter((p) => p.id !== id);
          console.log('Personaggio eliminato con successo');
        },
        error: (err: HttpErrorResponse) => {
          console.error(
            'Errore durante la cancellazione del personaggio:',
            err
          );
        },
      });
    }
  }

  loadImages(): void {
    // Immagini statiche per il carosello
    const images = [
      { url: 'assets/img/pg av.png', alt: 'Personaggio 1' },
      { url: 'assets/img/pg av.png', alt: 'Personaggio 2' },
      { url: 'assets/img/pg av.png', alt: 'Personaggio 3' },
      { url: 'assets/img/pg av.png', alt: 'Personaggio 4' },
      { url: 'assets/img/pg av.png', alt: 'Personaggio 5' },
      { url: 'assets/img/pg av.png', alt: 'Personaggio 6' },
    ];

    this.createImageGroups(images);
  }

  createImageGroups(images: { url: string; alt: string }[]): void {
    const groupSize = 3; // Mostra 3 immagini alla volta
    for (let i = 0; i < images.length; i += groupSize) {
      this.imageGroups.push(images.slice(i, i + groupSize));
    }
  }
}
