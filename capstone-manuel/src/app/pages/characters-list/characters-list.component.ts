import { IPersonaggioDTO } from './../../interface/i-personaggio-dto';
import { Component, OnInit } from '@angular/core';

import { PgService } from '../../services/pg.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-characters-list',
  templateUrl: './characters-list.component.html',
  styleUrl: './characters-list.component.scss',
})
export class CharactersListComponent implements OnInit {
  personaggi: IPersonaggioDTO[] = [];

  constructor(private pgService: PgService) {}

  ngOnInit(): void {
    this.loadPersonaggi();
  }
  loadPersonaggi(): void {
    this.pgService.getAllPersonaggi().subscribe({
      next: (personaggi: IPersonaggioDTO[]) => {
        this.personaggi = personaggi;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Errore nel recuperare i personaggi:', err);
      },
    });
  }
}
