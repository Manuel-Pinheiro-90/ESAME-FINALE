export interface IEventoCreate {
  titolo: string;
  descrizione: string;
  dataInizio: Date;
  dataFine: Date;
  luogo: string;
  numeroPartecipantiMax: number;
  immagineFile: File | null; //
}
