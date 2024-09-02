import { IRegistrazioneServizio } from './i-registrazione-servizio';

export interface IServizio {
  id: number;
  Nome: string;
  Descrizione: string;
  costo: number;
  registrazioniServizi: IRegistrazioneServizio[];
}
