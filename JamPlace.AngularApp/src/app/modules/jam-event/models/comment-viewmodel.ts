import { JamUserViewModel } from './jam-user-viewmodel';

export class CommentViewModel {
    id: number;
    content: string;
    eventId: number;
    date: Date;
    jamUser: JamUserViewModel;
}