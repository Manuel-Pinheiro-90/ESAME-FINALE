import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iEvento } from '../interface/ievento';
import { Observable } from 'rxjs';
import { IEventoCreate } from '../interface/ievento-create';
import { FormBuilder } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  private baseUrl = 'https://localhost:7236/api/eventi';
  constructor(private http: HttpClient) {}

  getEvents(): Observable<iEvento[]> {
    return this.http.get<iEvento[]>(`${this.baseUrl}`);
  }

  getEvent(id: number): Observable<iEvento> {
    return this.http.get<iEvento>(`${this.baseUrl}/${id}`);
  }

  createEvent(event: IEventoCreate): Observable<iEvento> {
    const formData = new FormData();
    formData.append('Titolo', event.titolo);
    formData.append('Descrizione', event.descrizione);
    formData.append('DataInizio', event.dataInizio.toISOString());
    formData.append('DataFine', event.dataFine.toISOString());
    formData.append('Luogo', event.luogo);
    formData.append(
      'NumeroPartecipantiMax',
      event.numeroPartecipantiMax.toString()
    );
    if (event.immagineFile) {
      formData.append('immagineFile', event.immagineFile);
    }

    return this.http.post<iEvento>(this.baseUrl, formData);
  }
  updateEvent(id: number, event: IEventoCreate): Observable<iEvento> {
    const formData = new FormData();
    formData.append('Titolo', event.titolo);
    formData.append('Descrizione', event.descrizione);
    formData.append('DataInizio', event.dataInizio.toISOString());
    formData.append('DataFine', event.dataFine.toISOString());
    formData.append('Luogo', event.luogo);
    formData.append(
      'NumeroPartecipantiMax',
      event.numeroPartecipantiMax.toString()
    );
    formData.append('ImmagineFile', event.immagineFile);
    return this.http.put<iEvento>(`${this.baseUrl}/${id}`, formData);
  }

  deleteEvent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
