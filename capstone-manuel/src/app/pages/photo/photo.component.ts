import { Component } from '@angular/core';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrl: './photo.component.scss',
})
export class PhotoComponent {
  images = [
    { src: 'assets/img/emp (1).jpg', alt: 'Foto 1' },
    { src: 'assets/img/ork.jpg', alt: 'Foto 2' },

    { src: 'assets/img/emp (2).jpg', alt: 'Foto 5' },
    { src: 'assets/img/foto-sezione-f/1.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/2.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/3.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/4.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/5.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/6.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/7.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/8.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/9.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/10.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/11.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/12.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/13.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/14.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/15.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/16.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/17.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/18.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/19.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/20.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/21.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/22.jpg', alt: 'Foto evento' },
    { src: 'assets/img/foto-sezione-f/23.jpg', alt: 'Foto evento' },

    { src: 'assets/img/foto-sezione-f/25.jpg', alt: 'Foto evento' },

    // Aggiungi altre immagini qui
  ];

  selectedImage: any = null;

  openImage(image: any): void {
    this.selectedImage = image;
    document.getElementById('imageModal')!.style.display = 'flex';
  }

  closeImage(): void {
    this.selectedImage = null;
    document.getElementById('imageModal')!.style.display = 'none';
  }
}
