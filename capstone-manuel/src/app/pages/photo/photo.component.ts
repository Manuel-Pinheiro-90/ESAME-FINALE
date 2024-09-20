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
    { src: 'assets/img/riv3.png', alt: 'Foto 3' },
    { src: 'assets/img/riv2.png', alt: 'Foto 4' },
    { src: 'assets/img/emp (1).jpg', alt: 'Foto 5' },
    { src: 'assets/img/emp (2).jpg', alt: 'Foto 1' },
    { src: 'assets/img/ork.jpg', alt: 'Foto 2' },
    { src: 'assets/img/no.png', alt: 'Foto 3' },
    { src: 'assets/img/riv2.png', alt: 'Foto 4' },
    { src: 'assets/img/emp (2).jpg', alt: 'Foto 5' },
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
