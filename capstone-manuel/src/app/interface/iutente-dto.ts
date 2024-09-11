import { IRuoloDTO } from './i-ruolo-dto';

export interface IUtenteDTO {
  id: number;
  nome: string;
  email: string;
  ruoli: IRuoloDTO[];
}
