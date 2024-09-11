import { Component } from '@angular/core';
import { IUser } from '../../../interface/i-user';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  newUser: Partial<IUser> = {};
  confirmPassword: string = '';
  passwordMismatch: boolean = false;

  constructor(private authSvc: AuthService, private router: Router) {}

  register() {
    if (this.newUser.PasswordHash !== this.confirmPassword) {
      this.passwordMismatch = true;
      return;
    }
    this.passwordMismatch = false;
    this.authSvc.register(this.newUser).subscribe(() => {
      this.router.navigate(['/auth']);
    });
  }
}
