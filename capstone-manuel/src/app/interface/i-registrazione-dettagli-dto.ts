import { IEventoDTO } from './i-evento-dto';
import { IPersonaggioDTO } from './i-personaggio-dto';
import { IServizioDTO } from './i-servizio-dto';
import { IUtenteDTO } from './iutente-dto';

export interface IRegistrazioneDettagliDTO {
  id: number;
  dataRegistrazione: Date;
  costoTotale: number;
  utente: IUtenteDTO;
  evento: IEventoDTO;
  personaggio?: IPersonaggioDTO;
  servizi: IServizioDTO[];
}
