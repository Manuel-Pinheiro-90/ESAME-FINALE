import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'newlinesToBr',
})
export class NewlinesToBrPipe implements PipeTransform {
  transform(value: string): string {
    return value ? value.replace(/\n/g, '<br>') : '';
  }
}
