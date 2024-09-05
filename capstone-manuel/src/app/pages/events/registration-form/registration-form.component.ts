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
      personaggioId: [null], // Campo opzionale per il personaggio
      serviziIds: [[]], // Campo opzionale per i servizi
      costoTotale: [0, Validators.required], // Costo obbligatorio
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
      id: 0, // L'ID viene gestito dal backend, quindi lo lasciamo a 0
      dataRegistrazione: new Date().toISOString(), // Data corrente
      eventoId: this.eventoId, // ID dell'evento catturato dalla route
      personaggioId: this.registrazioneForm.get('personaggioId')?.value, // Personaggio opzionale
      serviziIds: this.registrazioneForm.get('serviziIds')?.value ?? [], // Servizi selezionati
      costoTotale: this.registrazioneForm.get('costoTotale')?.value, // Costo totale
    };

    console.log('Dati inviati al backend:', registrazione);

    this.registrazionesvc.createRegistration(registrazione).subscribe({
      next: () => {
        console.log('Registrazione avvenuta con successo');
        this.router.navigate(['/events']); // Reindirizza alla pagina degli eventi
      },
      error: (err) => {
        console.error('Errore durante la registrazione:', err);
      },
    });
  }
}
