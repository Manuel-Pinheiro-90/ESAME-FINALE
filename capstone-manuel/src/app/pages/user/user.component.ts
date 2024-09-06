import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUser } from '../../interface/i-user';
import { UserService } from '../../services/user.service';
import { IUtenteDTO } from '../../interface/iutente-dto';
import { IUserProfile } from '../../interface/i-user-profile';
import { RegistraionService } from '../../services/registraion.service';
import { PgService } from '../../services/pg.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent implements OnInit {
  profileForm!: FormGroup;
  user: IUserProfile | null = null;

  constructor(
    private userService: UserService,
    private Regsvc: RegistraionService,
    private pgService: PgService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile(): void {
    this.userService.getProfile().subscribe({
      next: (user: IUserProfile) => {
        this.user = user;
      },
      error: (err) => {
        console.error("Errore nel recuperare i dati completi dell'utente", err);
      },
    });
  }
  deletePersonaggio(personaggio: number): void {
    this.pgService.deletePersonaggio(personaggio).subscribe({
      next: () => {
        this.getUserProfile();
        alert('Personaggio eliminato con successo');
        if (this.user) {
          this.user.personaggi = this.user.personaggi.filter(
            (p) => p.id !== personaggio
          );
        }
      },
      error: (err) => {
        console.error("Errore durante l'eliminazione del personaggio:", err);
      },
    });
  }

  //////////////////////////////////////////////////////
  deleteRegistrazione(registrazioneId: number): void {
    if (confirm('sei sicuro di  voler eliminare questa registrazione?')) {
      this.Regsvc.deleteRegistrazione(registrazioneId).subscribe({
        next: () => {
          this.getUserProfile();
          alert('Registrazione eliminata con successo');
        },
        error: (err) => {
          console.error(
            "Errore durante l'eliminazione della registrazione:",
            err
          );
        },
      });
    }
  }
}
//
