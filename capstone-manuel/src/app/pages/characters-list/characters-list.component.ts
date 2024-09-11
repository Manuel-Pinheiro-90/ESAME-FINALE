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
  personaggi: IPersonaggioDTO[] = [];
  descriptionVisible: { [key: number]: boolean } = {};
  constructor(private pgService: PgService) {}

  ngOnInit(): void {
    this.loadPersonaggi();
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
}
