import { IPersonaggioDTO } from './../interface/i-personaggio-dto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { IPersonaggioConUtenteDTO } from '../interface/i-personaggio-con-utente-dto';

@Injectable({
  providedIn: 'root',
})
export class PgService {
  private pgUrl = 'https://localhost:7236/api/Personaggi';
  constructor(private http: HttpClient) {}

  getPersonaggi(): Observable<IPersonaggioDTO[]> {
    return this.http.get<IPersonaggioDTO[]>(`${this.pgUrl}/personaggi`);
  }

  deletePersonaggio(personaggio: number): Observable<void> {
    return this.http.delete<void>(`${this.pgUrl}/${personaggio}`);
  }

  createPeresonaggio(
    personaggio: IPersonaggioDTO
  ): Observable<IPersonaggioDTO> {
    return this.http.post<IPersonaggioDTO>(this.pgUrl, personaggio);
  }

  getPersonaggio(id: number): Observable<IPersonaggioDTO> {
    return this.http.get<IPersonaggioDTO>(`${this.pgUrl}/${id}`);
  }

  getAllPersonaggi(): Observable<IPersonaggioDTO[]> {
    return this.http.get<IPersonaggioDTO[]>(this.pgUrl);
  }

  getPersonaggiConUtente(): Observable<IPersonaggioConUtenteDTO[]> {
    return this.http.get<IPersonaggioConUtenteDTO[]>(
      `${this.pgUrl}/personaggi-con-utenti`
    );
  }
}
