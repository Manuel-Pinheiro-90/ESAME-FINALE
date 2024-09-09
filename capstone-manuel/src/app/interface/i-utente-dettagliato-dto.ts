import { IPersonaggioDTO } from './i-personaggio-dto';
import { IRuoloDTO } from './i-ruolo-dto';

export interface IUtenteDettagliatoDTO {
  id: number;
  nome: string;
  email: string;
  foto: string;
  ruoli: IRuoloDTO[];
  personaggi: IPersonaggioDTO[];
}
