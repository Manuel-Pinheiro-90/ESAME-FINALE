import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPersonaggioDTO } from '../interface/i-personaggio-dto';
import { Observable } from 'rxjs';

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
}
