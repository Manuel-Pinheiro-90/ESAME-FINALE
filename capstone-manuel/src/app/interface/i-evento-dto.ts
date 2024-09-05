export interface IEventoDTO {
  id: number;
  titolo: string;
  descrizione: string;
  dataInizio: Date;
  dataFine: Date;
  luogo: string;
  numeroPartecipantiMax: number;
  immagineEvento?: string; // Facoltativo, quindi mettiamo "?" come in C#

  numeroRegistrazioni: number;
  numeroDocumenti: number;
  numeroPersonaggi: number;

  nomiUtentiRegistrati: string[];
}
