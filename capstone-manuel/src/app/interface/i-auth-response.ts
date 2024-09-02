import { IUser } from './i-user';
import { IUtenteDTO } from './iutente-dto';

export interface IAuthResponse {
  token: string;
  user: IUtenteDTO;
}
