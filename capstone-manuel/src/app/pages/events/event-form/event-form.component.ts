import { EventService } from './../../../services/event.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IEventoCreate } from '../../../interface/ievento-create';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrl: './event-form.component.scss',
})
export class EventFormComponent implements OnInit {
  eventForm!: FormGroup;
  selectedFile: File | null = null;
  isEditMode = false;
  eventId?: number | null = null;

  constructor(
    private fb: FormBuilder,
    private EventService: EventService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.eventForm = this.fb.group({
      titolo: ['', Validators.required],
      descrizione: ['', Validators.required],
      dataInizio: ['', Validators.required],
      dataFine: ['', Validators.required],
      luogo: ['', Validators.required],
      numeroPartecipantiMax: [0, [Validators.required, Validators.min(1)]],
      immagineFile: [null],
    });

    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.eventId = +id;
        this.loadEvent(this.eventId);
      }
    });
  }

  loadEvent(id: number): void {
    this.EventService.getEvent(id).subscribe((event) => {
      this.eventForm.patchValue({
        titolo: event.titolo,
        descrizione: event.descrizione,
        dataInizio: event.dataInizio,
        dataFine: event.dataFine,
        luogo: event.luogo,
        numeroPartecipantiMax: event.numeroPartecipantiMax,
      });
    });
  }
  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.eventForm.patchValue({ immagineFile: file });
    }
  }

  saveEvent(): void {
    if (this.eventForm.invalid) {
      return;
    }

    const event: IEventoCreate = {
      titolo: this.eventForm.get('titolo')?.value,
      descrizione: this.eventForm.get('descrizione')?.value,
      dataInizio: new Date(this.eventForm.get('dataInizio')?.value),
      dataFine: new Date(this.eventForm.get('dataFine')?.value),
      luogo: this.eventForm.get('luogo')?.value,
      numeroPartecipantiMax: this.eventForm.get('numeroPartecipantiMax')?.value,
      immagineFile: this.selectedFile || null,
    };

    if (this.isEditMode && this.eventId) {
      this.EventService.updateEvent(this.eventId, event).subscribe(() => {
        // Ricarica l'evento aggiornato e naviga alla lista degli eventi
        this.EventService.getEvent(this.eventId!).subscribe(() => {
          this.router.navigate(['/events']);
        });
      });
    } else {
      this.EventService.createEvent(event).subscribe(() => {
        // Forza il caricamento dell'intera lista di eventi dopo la creazione
        this.EventService.getEvents().subscribe(() => {
          this.router.navigate(['/events']);
        });
      });
    }
  }
}
