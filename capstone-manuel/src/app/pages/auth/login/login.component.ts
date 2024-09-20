import { Component } from '@angular/core';
import { IAuthData } from '../../../interface/i-auth-data';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  authData: IAuthData = {
    userName: '',
    password: '',
  };
  formSubmitted = false;
  loginError: boolean = false;
  errorMessage: string = '';

  constructor(private authSvc: AuthService, private router: Router) {}

  login() {
    this.formSubmitted = true;

    // Controllo che i campi username e password non siano vuoti
    if (this.authData.userName && this.authData.password) {
      this.authSvc.login(this.authData).subscribe({
        next: () => {
          this.loginError = false;
          this.router.navigate(['home']);
        },
        error: (err) => {
          this.loginError = true;
          this.errorMessage = 'Nome utente o password errati. Riprova.';
          console.error('Errore di login:', err);
        },
      });
    } else {
      this.loginError = true;
      this.errorMessage = 'Per favore, inserisci username e password.';
    }
  }
}
