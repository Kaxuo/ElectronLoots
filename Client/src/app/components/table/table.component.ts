import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { forkJoin, throwError } from 'rxjs';
import { Tables } from 'src/app/Models/Tables';
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
  @ViewChild('playerForm') playerForm;
  @ViewChild('floorForm') floorForm;
  @ViewChild('tableForm') tableForm;

  randomNumber: number;
  confirmClicked = false;
  cancelClicked = false;
  players: Players[] = [];
  floors: Floors[] = [];
  tables: Tables[] = [];
  addTableForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(15)]],
  });
  addPlayerForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(15)]],
  });
  addFloorForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(15)]],
  });
  modalAddTable: boolean = false;
  modalAddPlayer: boolean = false;
  modalAddFloor: boolean = false;

  constructor(
    private PlayersFloors: PlayersFloorsService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    let allPlayers = this.PlayersFloors.getAllPlayers();
    let allFloors = this.PlayersFloors.getAllFloors();
    let allTables = this.PlayersFloors.getAllTables();

    forkJoin([allPlayers, allFloors, allTables])
      .pipe(
        take(1),
        catchError((error) => {
          return throwError(error);
        })
      )
      .subscribe((result: [Players[], Floors[], Tables[]]) => {
        this.players = result[0].map((players) => {
          return {
            ...players,
            total: players.floors
              .map((floors) => floors.value)
              .reduce((a, b) => a + b, 0),
          };
        });
        this.floors = result[1];
        this.tables = result[2].map((table) => {
          return {
            ...table,
            players: table.players.map((players) => {
              return {
                ...players,
                total: players.floors
                  .map((floors) => floors.value)
                  .reduce((a, b) => a + b, 0),
              };
            }),
          };
        });
      });
  }

  addTable(el) {
    this.PlayersFloors.addTable(el)
      .pipe(take(1))
      .subscribe((table: Tables) => {
        this.closeModal();
        this.tables.push(table);
        setTimeout(() => {
          this.scrollToBottom();
        }, 100);
      });
  }

  addPlayer(el) {
    this.PlayersFloors.addPlayers(this.randomNumber, el)
      .pipe(take(1))
      .subscribe((newPlayer: Players) => {
        this.tables
          .find((table) => table.id == this.randomNumber)
          .players.push({ ...newPlayer, total: 0 });
        this.closeModal();
      });
  }

  addFloor(el) {
    this.PlayersFloors.addFloors(this.randomNumber, el)
      .pipe(take(1))
      .subscribe((newFloor: Floors) => {
        this.tables
          .find((table) => table.id == this.randomNumber)
          .floors.push(newFloor);
        this.tables
          .find((table) => table.id == this.randomNumber)
          .players.map((player) => {
            return player.floors.push({
              userId: player.userId,
              playerName: player.name,
              players: player,
              floorId: newFloor.floorId,
              floorName: newFloor.name,
              floors: newFloor,
              value: 0,
            });
          });
        this.closeModal();
      });
  }

  modifyValue(tableId, floor, event, indexPlayer, indexFloor) {
    let newValue = parseInt(event.target.value) || 0;
    this.PlayersFloors.modifyValue(tableId, floor.userId, floor.floorId, {
      value: newValue,
    }).subscribe(() => {
      this.tables.find((table) => table.id == tableId).players[
        indexPlayer
      ].floors[indexFloor].value = newValue;
      this.tables.find((table) => table.id == tableId).players[
        indexPlayer
      ].total = this.tables
        .find((table) => table.id == tableId)
        .players[indexPlayer].floors.map((floor) => floor.value)
        .reduce((a, b) => a + b, 0);
    });
  }

  showTables() {
    this.modalAddTable = true;
    setTimeout(() => {
      this.tableForm.nativeElement.focus();
    }, 200);
  }

  showPlayers(el) {
    this.randomNumber = el;
    this.modalAddPlayer = true;
    setTimeout(() => {
      this.playerForm.nativeElement.focus();
    }, 200);
  }

  showFloors(el) {
    this.modalAddFloor = true;
    this.randomNumber = el;
    setTimeout(() => {
      this.floorForm.nativeElement.focus();
    }, 200);
  }

  deleteTable(el) {
    this.PlayersFloors.deleteTable(el.id).subscribe(() => {
      this.confirmClicked = true;
      this.tables = this.tables.filter((table) => table.id != el.id);
      this.scrollToTop();
    });
  }

  deletePlayers(tableId, el) {
    this.PlayersFloors.deletePlayers(el.userId).subscribe(() => {
      this.confirmClicked = true;
      let singleTable = this.tables.find((table) => table.id == tableId.id);
      singleTable.players = singleTable.players.filter(
        (players) => players.userId != el.userId
      );
    });
  }

  deleteColumn(tableId, el) {
    this.PlayersFloors.deleteFloors(el.floorId).subscribe(() => {
      let singleTable = this.tables.find((table) => table.id == tableId.id);
      singleTable.floors = singleTable.floors.filter(
        (floors) => floors.floorId != el.floorId
      );
      singleTable.players = singleTable.players.map((player) => {
        let newFloor = player.floors.filter(
          (floor) => floor.floorId != el.floorId
        );
        return {
          ...player,
          floors: newFloor,
          total: newFloor
            .map((floor) => floor.value)
            .reduce((a, b) => a + b, 0),
        };
      });
      this.players = this.players.map((player) => {
        let newFloor = player.floors.filter(
          (floor) => floor.floorId != el.floorId
        );
        return {
          ...player,
          floors: newFloor,
          total: newFloor
            .map((floor) => floor.value)
            .reduce((a, b) => a + b, 0),
        };
      });
    });
  }

  closeModal() {
    this.modalAddPlayer = false;
    this.modalAddFloor = false;
    this.modalAddTable = false;
    this.randomNumber = 0;
    this.addPlayerForm.reset();
    this.addFloorForm.reset();
    this.addTableForm.reset();
  }

  @HostListener('click', ['$event'])
  onDocumentClick(event) {
    let modal = document.getElementsByClassName('modal')[0];
    if (event.target == modal) {
      this.closeModal();
    }
  }

  scrollToBottom() {
    window.scrollTo(0, document.body.scrollHeight);
  }
  scrollToTop() {
    window.scrollTo(0, 0);
  }
}
