import { Injectable } from '@angular/core';
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
}
