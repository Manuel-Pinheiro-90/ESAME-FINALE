import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IServizioDTO } from '../interface/i-servizio-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServiceService {
  private serviceUrl = 'https://localhost:7236/api/Servizi';
  constructor(private http: HttpClient) {}
  getServizi(): Observable<IServizioDTO[]> {
    return this.http.get<IServizioDTO[]>(`${this.serviceUrl}`);
  }
}
