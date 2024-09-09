import { Component, OnInit } from '@angular/core';
import { IPersonaggioDTO } from '../../../interface/i-personaggio-dto';
import { ActivatedRoute } from '@angular/router';
import { PgService } from '../../../services/pg.service';

@Component({
  selector: 'app-pg-detail',
  templateUrl: './pg-detail.component.html',
  styleUrl: './pg-detail.component.scss',
})
export class PgDetailComponent implements OnInit {
  personaggio: IPersonaggioDTO | null = null;
  personaggioId: number = 0;
  constructor(
    private route: ActivatedRoute, // Per ottenere l'ID del personaggio dall'URL
    private pgService: PgService // Per fare la chiamata al backend
  ) {}
  ngOnInit(): void {
    this.personaggioId = Number(this.route.snapshot.paramMap.get('id'));
    this.getPersonaggioDetail();
  }

  getPersonaggioDetail(): void {
    if (this.personaggioId) {
      this.pgService.getPersonaggio(this.personaggioId).subscribe({
        next: (data: IPersonaggioDTO) => {
          this.personaggio = data;
        },
        error: (err) => {
          console.error(
            'Errore durante il caricamento dei dettagli del personaggio',
            err
          );
        },
      });
    }
  }
}
