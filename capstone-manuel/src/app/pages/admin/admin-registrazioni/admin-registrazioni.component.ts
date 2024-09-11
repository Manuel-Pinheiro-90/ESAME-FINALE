import { Component, OnInit } from '@angular/core';
import { IRegistrazioneDettagliDTO } from '../../../interface/i-registrazione-dettagli-dto';
import { RegistraionService } from '../../../services/registraion.service';

@Component({
  selector: 'app-admin-registrazioni',
  templateUrl: './admin-registrazioni.component.html',
  styleUrl: './admin-registrazioni.component.scss',
})
export class AdminRegistrazioniComponent implements OnInit {
  registrazioni: IRegistrazioneDettagliDTO[] = [];

  constructor(private registrationService: RegistraionService) {}

  ngOnInit(): void {
    this.loadRegistrazioni();
  }

  loadRegistrazioni(): void {
    this.registrationService.getAllRegistrazioni().subscribe({
      next: (data: IRegistrazioneDettagliDTO[]) => {
        this.registrazioni = data;
      },
      error: (err) => {
        console.error('Errore nel recuperare le registrazioni:', err);
      },
    });
  }
  deleteRegistrazione(id: number): void {
    if (confirm('Sei sicuro di voler eliminare questa registrazione?')) {
      this.registrationService.deleteRegistrazione(id).subscribe({
        next: () => {
          this.registrazioni = this.registrazioni.filter((r) => r.id !== id);
          alert('Registrazione eliminata con successo.');
        },
        error: (err) => {
          console.error(
            'Errore durante la cancellazione della registrazione:',
            err
          );
        },
      });
    }
  }
}
