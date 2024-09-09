import { Component, OnInit } from '@angular/core';
import { IRegistrazioneDettagliDTO } from '../../../interface/i-registrazione-dettagli-dto';
import { ActivatedRoute } from '@angular/router';
import { RegistraionService } from '../../../services/registraion.service';

@Component({
  selector: 'app-register-detail',
  templateUrl: './register-detail.component.html',
  styleUrl: './register-detail.component.scss',
})
export class RegisterDetailComponent implements OnInit {
  registrazione!: IRegistrazioneDettagliDTO;
  totaleConServizi: number = 0;
  constructor(
    private route: ActivatedRoute,
    private registrazioneService: RegistraionService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.getRegistrazioneDettagli(parseInt(id));
    }
  }

  getRegistrazioneDettagli(id: number): void {
    this.registrazioneService.getRegistrazione(id).subscribe({
      next: (data) => {
        this.registrazione = data;
      },
      error: (err) => {
        console.error(
          'Errore nel recuperare i dettagli della registrazione:',
          err
        );
      },
    });
  }
}
