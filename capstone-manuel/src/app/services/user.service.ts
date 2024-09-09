import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, tap } from 'rxjs';
import { IUserProfile } from '../interface/i-user-profile';
import { IUserProfileUpdate } from '../interface/i-user-profile-update';
import { IUtenteDettagliatoDTO } from '../interface/i-utente-dettagliato-dto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private userApiUrl = 'https://localhost:7236/api/Auth/profile';
  private updateUrl = 'https://localhost:7236/api/Utenti/profile';
  private utentiUrl = 'https://localhost:7236/api/utenti';
  constructor(private http: HttpClient) {}

  getProfile(): Observable<IUserProfile> {
    return this.http.get<IUserProfile>(this.userApiUrl).pipe(
      tap((user) => console.log('Dati profilo utente recuperati:', user)) // Log della risposta API
    );
  }

  getAllUtenti(): Observable<IUtenteDettagliatoDTO[]> {
    return this.http.get<IUtenteDettagliatoDTO[]>(`${this.utentiUrl}`);
  }

  updateProfile(
    profileData: Partial<IUserProfileUpdate>,
    file?: File
  ): Observable<IUserProfileUpdate> {
    const formData = new FormData();
    console.log('profile fata qui', profileData);
    if (profileData.Nome) {
      formData.append('nome', profileData.Nome);
    }
    if (profileData.Email) {
      formData.append('email', profileData.Email);
    }
    if (profileData.Password) {
      formData.append('password', profileData.Password);
    }
    if (file) {
      formData.append('foto', file);
    }
    return this.http.put<IUserProfileUpdate>(`${this.updateUrl}`, formData);
  }

  deleteUtente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.utentiUrl}/${id}`);
  }
}
