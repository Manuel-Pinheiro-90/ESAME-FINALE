import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IUser } from '../interface/i-user';
import { IAuthResponse } from '../interface/i-auth-response';
import { IAuthData } from '../interface/i-auth-data';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { IUtenteDTO } from '../interface/iutente-dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  jwtHelper: JwtHelperService = new JwtHelperService();

  authSubject = new BehaviorSubject<null | IUtenteDTO>(null);
  syncIsLoggedIn: boolean = false;

  user$ = this.authSubject.asObservable();

  isLoggedIn$ = this.user$.pipe(
    map((user) => !!user),
    tap((user) => (this.syncIsLoggedIn = user))
  );

  constructor(private http: HttpClient, private router: Router) {
    this.restoreUser();
  }

  private registerUrl = 'https://localhost:7236/api/Auth/register';
  private loginUrl = 'https://localhost:7236/api/Auth/login';

  register(newUser: Partial<IUser>): Observable<IAuthResponse> {
    return this.http.post<IAuthResponse>(this.registerUrl, newUser);
  }

  login(authData: IAuthData): Observable<IAuthResponse> {
    return this.http.post<IAuthResponse>(this.loginUrl, authData).pipe(
      tap((data) => {
        this.authSubject.next(data.utente);
        localStorage.setItem('accessData', JSON.stringify(data));
        console.log('Utente loggato:', data.utente);
        this.router.navigate(['/']).then(() => {
          window.location.reload(); // Questo forza il refresh della navbar
        });
      })
    );
  }

  logout(): void {
    this.authSubject.next(null);
    localStorage.removeItem('accessData');
    this.router.navigate(['/home']);
  }

  autoLogout(): void {
    const accessData = this.getAccessData();
    if (!accessData) return;
    const expDate = this.jwtHelper.getTokenExpirationDate(
      accessData.token
    ) as Date;
    const expMs = expDate.getTime() - new Date().getTime();
    setTimeout(this.logout, expMs);
  }

  getAccessData(): IAuthResponse | null {
    const accessDataJson = localStorage.getItem('accessData');
    if (!accessDataJson) return null;
    const accessData: IAuthResponse = JSON.parse(accessDataJson);

    return accessData;
  }

  restoreUser(): void {
    const accessData = this.getAccessData();

    if (!accessData) return;
    if (this.jwtHelper.isTokenExpired(accessData.token)) return;
    this.authSubject.next(accessData.utente);
    this.autoLogout();
    console.log(accessData.token);
    console.log('Utente ripristinato dal localStorage:', accessData.utente);
  }

  ///////////////////////////////////////////////////////////////

  getRolesFromStorage(): string[] {
    const accessDataJson = localStorage.getItem('accessData');
    if (!accessDataJson) return [];
    const accessData: IAuthResponse = JSON.parse(accessDataJson);
    return accessData?.utente?.ruoli.map((role) => role.nome) || [];
  }

  hasRole(role: string): boolean {
    const roles = this.getRolesFromStorage();
    console.log('Ruoli utente dal localStorage:', roles);
    return roles.includes(role);
  }

  isAdmin(): boolean {
    const roles = this.getRolesFromStorage();
    return roles.includes('Admin');
  }
}
