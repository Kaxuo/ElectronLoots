import { PlayersFloors } from 'src/app/Models/PlayersFloors';

export interface Floors {
  floorId?: number;
  name: string;
  players?: PlayersFloors[];
}
