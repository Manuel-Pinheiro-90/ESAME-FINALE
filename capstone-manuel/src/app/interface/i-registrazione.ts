import { IPersonaggi } from './i-personaggi';
import { IRegistrazioneServizio } from './i-registrazione-servizio';
import { IUser } from './i-user';
import { iEvento } from './ievento';

export interface IRegistrazione {
  id: number;
  dataRegistrazione: Date;
  costoTotale: number;
  utenteId: number;
  utente: IUser;
  eventoId: number;
  evento: iEvento;
  personaggioId: number | null;
  personaggio: IPersonaggi;
  registrazioniServizi: IRegistrazioneServizio[];
}
