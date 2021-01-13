import { PlayersFloors } from 'src/app/Models/PlayersFloors';
export interface Players {
  userId?: number;
  name: string;
  floors?: PlayersFloors[];
  total?: number;
}
