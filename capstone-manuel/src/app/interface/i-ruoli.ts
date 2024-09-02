import { IUser } from './i-user';
export interface IRuoli {
  id: number;
  nome: string;
  user: IUser[];
}
