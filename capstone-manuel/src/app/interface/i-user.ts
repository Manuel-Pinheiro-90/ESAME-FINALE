import { IRuoli } from './i-ruoli';
import { IPersonaggi } from './i-personaggi';
import { iEvento } from './ievento';
import { IRegistrazione } from './i-registrazione';

export interface IUser {
  id: number;
  Nome: string;
  Email: string;
  PasswordHash: string;
  dataRegistrazione: Date;
  foto: string | null;
  ruoli: IRuoli[];
  eventiCreati: iEvento[];
  registrazioni: IRegistrazione[];
  personaggi: IPersonaggi[];
}
