import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewlinesToBrPipe } from './newlines-to-br.pipe';

@NgModule({
  declarations: [NewlinesToBrPipe], // Dichiara la pipe
  imports: [CommonModule],
  exports: [NewlinesToBrPipe], // Esporta la pipe per poterla usare in altri moduli
})
export class SharedModule {}
