import { Players } from './Players';
import { Floors } from './Floors';
import { PlayersFloors } from './PlayersFloors';
export interface Tables {
  name: string;
  id: number;
  players: Players[];
  floors: Floors[];
  playersFloors: PlayersFloors[];
}
