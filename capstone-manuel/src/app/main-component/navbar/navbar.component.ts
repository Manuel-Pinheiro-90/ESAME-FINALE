import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { IUtenteDTO } from '../../interface/iutente-dto';
import { AuthService } from '../../services/auth.service';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  user: IUtenteDTO | null = null;
  isDropdownOpen: boolean = false;
  isAdmin: boolean = false;
  isCollapsed: boolean = true;
  constructor(public authService: AuthService, private router: Router) {}

  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  ngOnInit(): void {
    this.authService.user$.subscribe((utente) => {
      this.user = utente;
      console.log('Utente ricevuto nella navbar:', this.user);
      this.isAdmin = this.authService.hasRole('Admin');
      console.log('Ruoli utente:', this.authService.getRolesFromStorage());
      console.log('È Admin:', this.isAdmin);
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/home']);
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }
}
