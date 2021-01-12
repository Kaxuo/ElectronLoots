import { Component, HostListener, OnInit } from '@angular/core';
import { forkJoin, throwError } from 'rxjs';
import { Floors } from 'src/app/Models/Floors';
import { Players } from 'src/app/Models/Players';
import { PlayersFloorsService } from 'src/app/services/players-floors.service';
import { catchError, take } from 'rxjs/operators';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements OnInit {
  players: Players[] = [];
  floors: Floors[] = [];
  addPlayerForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(15)]],
  });
  addFloorForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(15)]],
  });
  modalAddPlayer: boolean = false;
  modalAddFloor: boolean = false;

  constructor(
    private PlayersFloors: PlayersFloorsService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    let allPlayers = this.PlayersFloors.getAllPlayers();
    let allFloors = this.PlayersFloors.getAllFloors();

    forkJoin([allPlayers, allFloors])
      .pipe(
        take(1),
        catchError((error) => {
          return throwError(error);
        })
      )
      .subscribe((result: [Players[], Floors[]]) => {
        this.players = result[0].map((players) => {
          return {
            ...players,
            total: players.floors
              .map((floors) => floors.value)
              .reduce((a, b) => a + b, 0),
          };
        });
        this.floors = result[1];
        console.log(this.floors);
        console.log(this.players);
      });
  }

  addPlayer(el) {
    this.PlayersFloors.addPlayers(el)
      .pipe(take(1))
      .subscribe((newPlayer: Players) => {
        this.closeModal();
        this.players.push({ ...newPlayer, total: 0 });
      });
  }

  addFloor(el) {
    this.PlayersFloors.addFloors(el)
      .pipe(take(1))
      .subscribe((newFloor: Floors) => {
        this.floors.push(newFloor);
        this.players = this.players.map((players: Players) => {
          players.floors.push({
            userId: players.userId,
            floorId: newFloor.floorId,
            value: 0,
          });
          return players;
        });
        this.closeModal();
      });
  }

  showPlayers() {
    this.modalAddPlayer = true;
  }

  showFloors() {
    this.modalAddFloor = true;
  }

  deletePlayers(el) {
    if (confirm(`Are you sure you want to delete ${el.name}`)) {
      this.PlayersFloors.deletePlayers(el.userId).subscribe(() => {
        this.players = this.players.filter(
          (player) => player.userId != el.userId
        );
      });
    }
  }

  deleteColumn(el) {
    if (confirm(`Are you sure you want to delete the ${el.name}`)) {
      this.PlayersFloors.deleteFloors(el.floorId).subscribe(() => {
        this.floors = this.floors.filter(
          (floor) => floor.floorId != el.floorId
        );
        this.players = this.players.map((player) => {
          let newFloor = player.floors.filter(
            (floor) => floor.floorId != el.floorId
          );
          return { ...player, floors: newFloor };
        });
      });
    }
  }

  closeModal() {
    this.modalAddPlayer = false;
    this.modalAddFloor = false;
    this.addPlayerForm.reset();
  }

  @HostListener('click', ['$event'])
  onDocumentClick(event) {
    let modal = document.getElementsByClassName('modal')[0];
    if (event.target == modal) {
      this.closeModal();
    }
  }
}
