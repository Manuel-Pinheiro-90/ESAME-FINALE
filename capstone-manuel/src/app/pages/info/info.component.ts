import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrl: './info.component.scss',
})
export class InfoComponent implements OnInit {
  ngOnInit(): void {
    this.checkScroll();
  }

  @HostListener('window:scroll', [])
  onWindowScroll(): void {
    this.checkScroll();
  }

  checkScroll(): void {
    const elements = document.querySelectorAll('.scroll-animation, .fade-in'); //eliminare il queryselectorall
    const windowHeight = window.innerHeight;

    elements.forEach((element: any) => {
      const elementTop = element.getBoundingClientRect().top;
      if (elementTop < windowHeight - 50) {
        element.classList.add('show');
      }
    });
  }
}
