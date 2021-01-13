import { Floors } from './Floors';
import { Players } from './Players';
export interface PlayersFloors {
  userId?: number;
  playerName?: string;
  players?: Players;
  floorId?: number;
  floorName?: string;
  floors?: Floors;
  value: number;
}
