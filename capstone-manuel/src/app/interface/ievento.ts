export interface iEvento {
  id: number;
  titolo: string;
  descrizione: string;
  dataInizio: Date;
  dataFine: Date;
  luogo: string;
  numeroPartecipantiMax: number;
  immagineEvento?: string;
  numeroRegistrazioni: number;
  numeroDocumenti: number;
  numeroPersonaggi: number;
  nomiUtentiRegistrati: string[];
}
