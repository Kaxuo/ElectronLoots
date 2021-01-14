import { Injectable } from '@angular/core';
import { Players } from '../Models/Players';
import { WebRequestService } from './web-request.service';

@Injectable({
  providedIn: 'root',
})
export class PlayersFloorsService {
  constructor(private webRequest: WebRequestService) {}

  getAllTables() {
    return this.webRequest.getAllTables();
  }

  addTable(payload: { name: string }) {
    return this.webRequest.addTable(payload);
  }

  deleteTable(id: number) {
    return this.webRequest.deleteTables(id);
  }

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

  addPlayers(id: number, payload: Players) {
    return this.webRequest.addPlayers(id, payload);
  }

  addFloors(id: number, payload: Players) {
    return this.webRequest.addFloors(id, payload);
  }

  modifyValue(
    tableId: number,
    userId: number,
    floorId: number,
    payload: { value: number }
  ) {
    return this.webRequest.modifyValue(tableId, userId, floorId, payload);
  }
}
