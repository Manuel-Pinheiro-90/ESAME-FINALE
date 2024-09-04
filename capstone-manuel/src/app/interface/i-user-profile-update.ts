export interface IUserProfileUpdate {
  Nome: string;
  Email: string;
  Password?: string;
  Foto?: File | null;
}
