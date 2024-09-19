import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistraionService } from '../../../services/registraion.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IRegistrazioneCreate } from '../../../interface/i-registrazione-create';
import { ServiceService } from '../../../services/service.service';
import { PgService } from '../../../services/pg.service';
import { IPersonaggioDTO } from '../../../interface/i-personaggio-dto';
import { IServizioDTO } from '../../../interface/i-servizio-dto';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrl: './registration-form.component.scss',
})
export class RegistrationFormComponent implements OnInit {
  registrazioneForm!: FormGroup;
  eventoId!: number;
  personaggi: IPersonaggioDTO[] = []; // Elenco dei personaggi
  servizi: IServizioDTO[] = []; // Elenco dei servizi
  selectedServices: number[] = [];

  constructor(
    private fb: FormBuilder,
    private registrazionesvc: RegistraionService,
    private router: Router,
    private route: ActivatedRoute,
    private servicesvc: ServiceService,
    private pgService: PgService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    // Recupera l'eventoId dalla rotta
    this.route.paramMap.subscribe((params) => {
      const id = params.get('eventoId');
      if (id) {
        this.eventoId = +id;
      }
    });

    // Inizializza il form
    this.registrazioneForm = this.fb.group({
      personaggioId: [''],
      serviziIds: [[]],
      costoTotale: [30, Validators.required],
    });

    // Carica personaggi e servizi
    this.loadPersonaggi();
    this.loadServizi();
  }

  // Funzione per caricare i personaggi
  loadPersonaggi(): void {
    this.pgService.getPersonaggi().subscribe({
      next: (personaggi: IPersonaggioDTO[]) => {
        this.personaggi = personaggi;
      },
      error: (err: HttpErrorResponse) => {
        console.error('Errore nel recuperare i personaggi:', err);
      },
    });
  }

  // Funzione per caricare i servizi
  loadServizi(): void {
    this.servicesvc.getServizi().subscribe({
      next: (servizi) => {
        this.servizi = servizi;
      },
      error: (err) => {
        console.error('Errore nel recuperare i servizi:', err);
      },
    });
  }

  // Funzione per inviare i dati del form
  onSubmit(): void {
    if (this.registrazioneForm.invalid) {
      return;
    }

    const registrazione: IRegistrazioneCreate = {
      id: 0,
      dataRegistrazione: new Date().toISOString(), // Data corrente
      eventoId: this.eventoId,

      serviziIds: this.registrazioneForm.get('serviziIds')?.value ?? [], // Servizi selezionati
      costoTotale: this.registrazioneForm.get('costoTotale')?.value,
    };
    ////////////////////////////////////////////////////////////////////////////////////////////////
    if (this.registrazioneForm.get('personaggioId')?.value) {
      registrazione.personaggioId =
        this.registrazioneForm.get('personaggioId')?.value; // Personaggio selezionato se presente
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    console.log('Dati inviati al backend:', registrazione);

    this.registrazionesvc.createRegistration(registrazione).subscribe({
      next: () => {
        console.log('Registrazione avvenuta con successo');
        this.router.navigate(['/events']);
      },
      error: (err) => {
        if (err.status === 400) {
          alert('Sei già registrato a questo evento.');
        } else {
          console.error('Errore durante la registrazione:', err);
        }
      },
    });
  }
  onServiceChange(servizioId: number, event: Event): void {
    const inputElement = event.target as HTMLInputElement; // Cast a HTMLInputElement
    if (inputElement.checked) {
      this.selectedServices.push(servizioId); // Aggiungi servizio selezionato
    } else {
      const index = this.selectedServices.indexOf(servizioId);
      if (index > -1) {
        this.selectedServices.splice(index, 1); // Rimuovi servizio deselezionato
      }
    }
    this.registrazioneForm.patchValue({ serviziIds: this.selectedServices });
  }
}
