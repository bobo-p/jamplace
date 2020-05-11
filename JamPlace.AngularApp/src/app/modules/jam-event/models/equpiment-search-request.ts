export class EqupimentSearchRequest{
    searchText: string;
    eventId: number;
    constructor(txt?: string, id?: number)
    {
        this.searchText = txt;
        this.eventId = id;
    }
}
