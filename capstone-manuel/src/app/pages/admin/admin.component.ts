import { Component, OnInit } from '@angular/core';
import { IUtenteDettagliatoDTO } from '../../interface/i-utente-dettagliato-dto';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent implements OnInit {
  utenti: IUtenteDettagliatoDTO[] = [];
  ruoloDaAggiungere: string = '';
  ruoliDisponibili: string[] = ['Admin', 'Utente'];
  ruoliDaAggiungere: { [key: number]: string } = {};
  constructor(
    private usersService: UserService,
    private authsvc: AuthService
  ) {}

  ngOnInit(): void {
    this.loadAllUtenti();
    console.log(this.authsvc.getAccessData());
  }

  loadAllUtenti(): void {
    this.usersService.getAllUtenti().subscribe({
      next: (data: IUtenteDettagliatoDTO[]) => {
        this.utenti = data;
      },
      error: (err) => {
        console.error('Errore nel recuperare gli utenti:', err);
      },
    });
  }
  aggiungiRuolo(id: number): void {
    const ruolo = this.ruoliDaAggiungere[id]; // Recupera il ruolo specifico per l'utente
    if (ruolo && ruolo.trim()) {
      this.usersService.aggiungiRuolo(id, ruolo).subscribe({
        next: () => {
          alert(`Ruolo '${ruolo}' aggiunto con successo.`);
          this.loadAllUtenti(); // Ricarica la lista degli utenti
        },
        error: (err) => {
          console.error("Errore durante l'aggiunta del ruolo:", err);
        },
      });
    } else {
      alert('Inserisci il nome del ruolo da aggiungere.');
    }
  }
  rimuoviRuolo(id: number, ruoloNome: string): void {
    this.usersService.rimuoviRuolo(id, ruoloNome).subscribe({
      next: () => {
        alert(`Ruolo '${ruoloNome}' rimosso con successo.`);
        this.loadAllUtenti(); // Ricarica la lista degli utenti
      },
      error: (err) => {
        console.error('Errore durante la rimozione del ruolo:', err);
      },
    });
  }

  deleteUser(id: number): void {
    if (confirm('Sei sicuro di voler eliminare questo utente?')) {
      this.usersService.deleteUtente(id).subscribe({
        next: () => {
          this.utenti = this.utenti.filter((user) => user.id !== id); // Rimuove l'utente dalla lista
          alert('Utente eliminato con successo.');
        },
        error: (err) => {
          console.error("Errore durante la cancellazione dell'utente:", err);
        },
      });
    }
  }
}
