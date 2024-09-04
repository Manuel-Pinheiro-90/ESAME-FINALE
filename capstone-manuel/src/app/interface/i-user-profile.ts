export interface IUserProfile {
  name: string;
  email: string;
  foto: string | null;
  registrazioni: any[];
  personaggi: {
    id: number;
    nome: string;
    descrizione: string;
  }[];
  roles: string[];
}
