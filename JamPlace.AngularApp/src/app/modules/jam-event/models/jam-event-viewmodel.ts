import { Address } from '../../shared/addres';
import { JamUserViewModel } from './jam-user-viewmodel';
import { SongViewModel } from './song-vewmodel';
import { EquipmentViewModel } from './equipment-vewmodel';

export class JamEventViewModel {
    id: number;
    name: string;
    size: string;
    description: string;
    address: Address;
    users: JamUserViewModel[];
    songs: SongViewModel[];
    neededEquipment: EquipmentViewModel[];
    date: Date;
}