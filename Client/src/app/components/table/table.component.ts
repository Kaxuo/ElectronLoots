import { Component, HostListener, OnInit } from '@angular/core';
import { forkJoin, throwError } from 'rxjs';
import { Floors } from 'src/app/Models/Floors';
import { Players } from 'src/app/Models/Players';
import { PlayersFloorsService } from 'src/app/services/players-floors.service';
import { catchError, take } from 'rxjs/operators';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements OnInit {
  players: Players[] = [];
  floors: Floors[] = [];
  addPlayers: boolean = true;
  addFloors: boolean = false;

  constructor(private PlayersFloors: PlayersFloorsService) {}

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
        this.players = result[0].map((x) => {
          return {
            ...x,
            total: x.floors.map((x) => x.value).reduce((a, b) => a + b, 0),
          };
        });
        this.floors = result[1];
      });
  }

  showPlayers() {
    this.addPlayers = true;
  }

  showFloors() {
    this.addFloors = true;
  }

  closeModal() {
    this.addPlayers = false;
    this.addFloors = false;
  }

  @HostListener('click', ['$event'])
  onDocumentClick(event) {
    let modal = document.getElementsByClassName('modal')[0];
    if (event.target == modal) {
      this.addPlayers = false;
    }
    console.log(event.target);
  }
}
