import { Component, OnInit } from '@angular/core';
import { IUserProfileUpdate } from '../../../interface/i-user-profile-update';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.scss',
})
export class EditUserComponent implements OnInit {
  profileForm!: FormGroup;
  selectedFile: File | null = null;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Inizializza il form con i campi nome, email, password, e immagine
    this.profileForm = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [''], // Password opzionale
      immagineFile: [null], // Campo per l'immagine opzionale
    });

    // Carica i dati dell'utente quando il componente viene inizializzato
    this.loadUserProfile();
  }

  // Funzione per caricare i dati utente e popolare il form
  loadUserProfile(): void {
    this.userService.getProfile().subscribe((user) => {
      this.profileForm.patchValue({
        nome: user.name,
        email: user.email,
      });
    });
  }

  // Funzione per gestire il caricamento di un file
  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.profileForm.patchValue({ immagineFile: file });
    }
  }

  // Funzione per salvare i dati del profilo
  saveProfile(): void {
    console.log('Funzione saveProfile chiamata');

    // Log dello stato del form e degli errori
    console.log('Stato del form:', this.profileForm.status);
    console.log('Errori del form:', this.profileForm.errors);

    if (this.profileForm.invalid) {
      console.error('Il form non è valido.');
      console.log(
        'Contenuto dei controlli del form:',
        this.profileForm.controls
      );
      return;
    }

    // Se il form è valido, continua con la logica di aggiornamento
    const updatedProfile: IUserProfileUpdate = {
      Nome: this.profileForm.get('nome')?.value,
      Email: this.profileForm.get('email')?.value,
      Password: this.profileForm.get('password')?.value || '',
      Foto: this.selectedFile,
    };

    const fileToUpload: File | undefined = this.selectedFile
      ? this.selectedFile
      : undefined;

    // Log per verificare il file selezionato
    if (fileToUpload) {
      console.log('File da caricare:', fileToUpload);
    } else {
      console.log('Nessun file selezionato');
    }

    // Chiama il servizio per aggiornare il profilo
    this.userService.updateProfile(updatedProfile, fileToUpload).subscribe({
      next: () => {
        console.log('Aggiornamento riuscito!');
        this.router.navigate(['/user']);
      },
      error: (err) => {
        console.error("Errore durante l'aggiornamento del profilo:", err);
      },
    });
  }
}
