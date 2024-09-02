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

  constructor(private authSvc: AuthService, private router: Router) {}

  register() {
    this.authSvc.register(this.newUser).subscribe(() => {
      this.router.navigate(['/login']);
    });
  }
}
