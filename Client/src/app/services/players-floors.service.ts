import { Injectable } from '@angular/core';
import { Players } from '../Models/Players';
import { WebRequestService } from './web-request.service';

@Injectable({
  providedIn: 'root',
})
export class PlayersFloorsService {
  constructor(private webRequest: WebRequestService) {}

  getAllPlayers() {
    return this.webRequest.getAllPlayers();
  }

  getAllFloors() {
    return this.webRequest.getAllFloors();
  }

  deletePlayers(id: number) {
    return this.webRequest.deletePlayers(id);
  }

  deleteFloors(id: number) {
    return this.webRequest.deleteFloors(id);
  }

  addPlayers(payload: Players) {
    return this.webRequest.addPlayers(payload);
  }

  addFloors(payload: Players) {
    return this.webRequest.addFloors(payload);
  }

  modifyValue(userId: number, floorId: number, payload: { value: number }) {
    return this.webRequest.modifyValue(userId, floorId, payload);
  }
}
