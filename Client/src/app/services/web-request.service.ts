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

  addPlayers(payload: Players) {
    return this.http.post(
      `${this.ROOT_URL}/api/joinedtable/addplayers`,
      payload
    );
  }

  addFloors(payload: Floors) {
    return this.http.post(
      `${this.ROOT_URL}/api/joinedtable/addfloors`,
      payload
    );
  }

  modifyValue(userId: number, floorId: number, payload: { value: number }) {
    return this.http.put(
      `${this.ROOT_URL}/api/joinedtable/${userId}/${floorId}/update`,
      payload
    );
  }
}
