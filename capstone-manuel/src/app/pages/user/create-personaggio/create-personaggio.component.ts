import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PgService } from '../../../services/pg.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-personaggio',
  templateUrl: './create-personaggio.component.html',
  styleUrl: './create-personaggio.component.scss',
})
export class CreatePersonaggioComponent {
  createPersonaggioForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private pgService: PgService, // Usa il PgService
    private router: Router
  ) {
    this.createPersonaggioForm = this.fb.group({
      nome: ['', Validators.required],
      descrizione: ['', Validators.required],
    });
  }
  // Metodo per inviare la richiesta di creazione del personaggio
  createPersonaggio(): void {
    if (this.createPersonaggioForm.invalid) {
      return;
    }

    const newPersonaggio = this.createPersonaggioForm.value;

    this.pgService.createPeresonaggio(newPersonaggio).subscribe({
      next: () => {
        alert('Personaggio creato con successo');
        this.router.navigate(['/user']);
      },
      error: (err) => {
        console.error('Errore durante la creazione del personaggio:', err);
      },
    });
  }
}
