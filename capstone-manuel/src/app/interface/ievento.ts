export interface iEvento {
  id: number
  titolo: string
  descrizione: Date
  dataInizio: Date
  dataFine: string
  luogo: string
  numeroPartecipantiMax: number
  immagineEvento?: string
  numeroRegistrazioni: number
  numeroDocumenti: number
  numeroPersonaggi: number
  nomiUtentiRegistrati: string[]
}
