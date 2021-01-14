import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Players } from 'src/app/Models/Players';
import { Floors } from '../Models/Floors';

@Injectable({
  providedIn: 'root',
})
export class WebRequestService {
  readonly ROOT_URL;

  constructor(private http: HttpClient) {
    this.ROOT_URL = 'http://localhost:5000';
    // this.ROOT_URL = 'http://localhost:8001';
  }

  getAllTables() {
    return this.http.get(`${this.ROOT_URL}/api/tables`);
  }

  deleteTables(id: number) {
    return this.http.delete(`${this.ROOT_URL}/api/tables/${id}`);
  }

  addTable(payload: { name: string }) {
    return this.http.post(`${this.ROOT_URL}/api/tables`, payload);
  }

  getAllPlayers() {
    return this.http.get(`${this.ROOT_URL}/api/players`);
  }

  getAllFloors() {
    return this.http.get(`${this.ROOT_URL}/api/floors`);
  }

  deletePlayers(id: number) {
    return this.http.delete(`${this.ROOT_URL}/api/players/${id}`);
  }

  deleteFloors(id: number) {
    return this.http.delete(`${this.ROOT_URL}/api/floors/${id}`);
  }

  addPlayers(id: number, payload: Players) {
    return this.http.post(
      `${this.ROOT_URL}/api/tables/${id}/addplayers`,
      payload
    );
  }

  addFloors(id: number, payload: Floors) {
    return this.http.post(
      `${this.ROOT_URL}/api/tables/${id}/addfloors`,
      payload
    );
  }

  modifyValue(tableId:number,userId: number, floorId: number, payload: { value: number }) {
    return this.http.put(
      `${this.ROOT_URL}/api/joined/${tableId}/${userId}/${floorId}`,
      payload
    );
  }
}
