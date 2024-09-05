import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegistrazioneCreate } from '../interface/i-registrazione-create';
import { Observable } from 'rxjs';
import { IRegistrazioneDettagliDTO } from '../interface/i-registrazione-dettagli-dto';

@Injectable({
  providedIn: 'root',
})
export class RegistraionService {
  private RegUrl = 'https://localhost:7236/api/registrazioni';
  constructor(private http: HttpClient) {}

  createRegistration(
    registrazione: IRegistrazioneCreate
  ): Observable<IRegistrazioneDettagliDTO> {
    return this.http.post<IRegistrazioneDettagliDTO>(
      `${this.RegUrl}`,
      registrazione
    );
  }

  /////////////////////////////////////////////////////
  deleteRegistrazione(registrazioneId: number): Observable<void> {
    return this.http.delete<void>(`${this.RegUrl}/${registrazioneId}`);
  }
}
