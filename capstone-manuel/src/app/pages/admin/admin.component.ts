import { Component, OnInit } from '@angular/core';
import { IUtenteDettagliatoDTO } from '../../interface/i-utente-dettagliato-dto';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent implements OnInit {
  utenti: IUtenteDettagliatoDTO[] = [];

  constructor(private usersService: UserService) {}

  ngOnInit(): void {
    this.loadAllUtenti();
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
