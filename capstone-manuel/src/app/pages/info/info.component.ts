import {
  AfterViewInit,
  Component,
  ElementRef,
  HostListener,
  OnInit,
  QueryList,
  ViewChildren,
} from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrl: './info.component.scss',
})
export class InfoComponent implements AfterViewInit {
  @ViewChildren('fadeIn') fadeInElements!: QueryList<ElementRef>;
  // Seleziona gli elementi con la classe scroll-animation
  @ViewChildren('scrollAnimation') scrollElements!: QueryList<ElementRef>;

  constructor(private el: ElementRef) {}

  ngAfterViewInit(): void {
    const observerOptions = { threshold: 0.4 };
    const observer = new IntersectionObserver((entries, observer) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('show');
          observer.unobserve(entry.target);
        }
      });
    }, observerOptions);

    this.fadeInElements.forEach((element) =>
      observer.observe(element.nativeElement)
    );
    this.scrollElements.forEach((element) =>
      observer.observe(element.nativeElement)
    );
  }
}
