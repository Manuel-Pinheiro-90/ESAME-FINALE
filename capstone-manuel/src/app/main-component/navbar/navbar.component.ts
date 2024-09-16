import { Router } from '@angular/router';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { IUtenteDTO } from '../../interface/iutente-dto';
import { AuthService } from '../../services/auth.service';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  user: IUtenteDTO | null = null;
  isDropdownOpen: boolean = false;
  isAdmin: boolean = false;
  isCollapsed: boolean = true;
  constructor(
    public authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {
    this.authService.user$.subscribe((utente) => {
      console.log('Utente attuale:', utente); // <-- Log per debug
    });
  }

  ngOnInit(): void {
    this.authService.user$.subscribe((utente) => {
      this.user = utente;

      // sistema di recupero ruolo senza refresh
      if (this.user) {
        this.user.ruoli.forEach((ruolo) => {
          if (ruolo.nome == 'Admin') {
            this.isAdmin = true;
          }
        });
        console.log(
          'Ruoli dopo il login:',
          this.authService.getRolesFromStorage()
        ); // Verifica se l'utente è admin
      } else {
        this.isAdmin = false;
      }
      this.cdr.detectChanges();
    });
  }

  logout(): void {
    this.authService.logout();
    this.isAdmin = false;
    this.router.navigate(['/home']);
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }

  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }
}
