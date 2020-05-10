import { JamUserModel } from '../../shared/jam-user-info';
import { Address } from '../../shared/addres';

export class UserSpecificJamEvent{
    id: number;
    name: string;
    size: string;
    description: string;
    address: Address;
    date: Date;
    creator: JamUserModel
}