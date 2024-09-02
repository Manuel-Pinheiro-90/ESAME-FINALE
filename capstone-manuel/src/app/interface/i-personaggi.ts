import { IUser } from './i-user';
import { iEvento } from './ievento';

export interface IPersonaggi {
  id: number;

  nome: string;
  descrizione: string;
  utente: IUser;
  EventoId: number;
  Evento: iEvento;
}
