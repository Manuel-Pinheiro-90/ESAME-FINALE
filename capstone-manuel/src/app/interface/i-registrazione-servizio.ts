import { IRegistrazione } from './i-registrazione';
import { IServizio } from './i-servizio';

export interface IRegistrazioneServizio {
  registrazioneId: number;
  registrazione: IRegistrazione;
  servizioId: number;
  servizio: IServizio;
}
