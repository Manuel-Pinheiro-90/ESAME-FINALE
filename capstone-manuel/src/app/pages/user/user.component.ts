import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUser } from '../../interface/i-user';
import { UserService } from '../../services/user.service';
import { IUtenteDTO } from '../../interface/iutente-dto';
import { IUserProfile } from '../../interface/i-user-profile';

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

    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getUserProfile();
  }

  getUserProfile(): void {
    this.userService.getProfile().subscribe({
      next: (user: IUserProfile) => {
        this.user = user;
        console.log('Utente loggato:', this.user); // Logga l'oggetto
        console.log(
          'Dati completi utente:',
          JSON.stringify(this.user, null, 2)
        );
      },
      error: (err) => {
        console.error("Errore nel recuperare i dati completi dell'utente", err);
      },
    });
  }
}
