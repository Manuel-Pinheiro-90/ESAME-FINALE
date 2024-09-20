import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iEvento } from '../interface/ievento';
import { BehaviorSubject, Observable } from 'rxjs';
import { IEventoCreate } from '../interface/ievento-create';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  private eventSubject = new BehaviorSubject<iEvento[]>([]);
  event$ = this.eventSubject.asObservable();

  private baseUrl = 'https://localhost:7236/api/eventi';

  constructor(private http: HttpClient) {
    this.loadInitialEvents();
  }

  loadInitialEvents(): void {
    this.http.get<iEvento[]>(this.baseUrl).subscribe((data) => {
      // Inizializza numeroRegistrazioni a 0 se è null o undefined
      data.forEach((evento) => {
        if (evento.numeroRegistrazioni == null) {
          evento.numeroRegistrazioni = 0;
        }
      });
      this.eventSubject.next(data);
    });
  }

  getEvents(): Observable<iEvento[]> {
    if (this.eventSubject.value.length === 0) {
      return this.http.get<iEvento[]>(this.baseUrl).pipe(
        tap((data) => {
          // Inizializza numeroRegistrazioni a 0 se è null o undefined
          data.forEach((evento) => {
            if (evento.numeroRegistrazioni == null) {
              evento.numeroRegistrazioni = 0;
            }
          });
          this.eventSubject.next(data);
        })
      );
    }

    return this.event$;
  }

  getEvent(id: number): Observable<iEvento> {
    return this.http.get<iEvento>(`${this.baseUrl}/${id}`).pipe(
      tap((evento) => {
        // Inizializza numeroRegistrazioni a 0 se è null o undefined
        if (evento.numeroRegistrazioni == null) {
          evento.numeroRegistrazioni = 0;
        }
      })
    );
  }

  createEvent(event: IEventoCreate): Observable<iEvento> {
    const formData = this.createFormData(event);
    return this.http.post<iEvento>(this.baseUrl, formData).pipe(
      tap((newEvent) => {
        // Inizializza numeroRegistrazioni a 0 per i nuovi eventi
        if (newEvent.numeroRegistrazioni == null) {
          newEvent.numeroRegistrazioni = 0;
        }
        const updatedEvents = [...this.eventSubject.value, newEvent];
        this.eventSubject.next(updatedEvents);
      })
    );
  }

  updateEvent(id: number, event: IEventoCreate): Observable<iEvento> {
    const formData = this.createFormData(event);
    return this.http.put<iEvento>(`${this.baseUrl}/${id}`, formData).pipe(
      tap((updatedEvent) => {
        // Assicurati che numeroRegistrazioni sia inizializzato correttamente
        if (updatedEvent.numeroRegistrazioni == null) {
          updatedEvent.numeroRegistrazioni = 0;
        }

        // Aggiorna direttamente l'evento nell'array del BehaviorSubject
        const updatedEvents = this.eventSubject.value.map((e) =>
          e.id === id ? updatedEvent : e
        );

        // Aggiorna il BehaviorSubject con il nuovo array di eventi
        this.eventSubject.next([...updatedEvents]);
      })
    );
  }

  deleteEvent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const updatedEvents = this.eventSubject.value.filter(
          (e) => e.id !== id
        );
        this.eventSubject.next(updatedEvents);
      })
    );
  }

  private createFormData(event: IEventoCreate): FormData {
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
      formData.append('ImmagineFile', event.immagineFile);
    }
    return formData;
  }

  resetCache(): void {
    this.eventSubject.next([]);
    this.loadInitialEvents();
  }

  updateEventInSubject(updatedEvent: iEvento): void {
    const events = this.eventSubject.value.map((event) =>
      event.id === updatedEvent.id ? updatedEvent : event
    );
    this.eventSubject.next(events);
  }
}
