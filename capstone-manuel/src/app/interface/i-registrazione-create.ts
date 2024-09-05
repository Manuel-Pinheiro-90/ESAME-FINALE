export interface IRegistrazioneCreate {
  id: number; // Anche se non lo invii esplicitamente, il backend lo gestisce
  dataRegistrazione: string;
  costoTotale: number;
  eventoId: number;
  personaggioId?: number;
  serviziIds?: number[];
}
