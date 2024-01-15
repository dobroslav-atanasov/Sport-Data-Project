import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Game, GameItemComponent } from '../game-item/game-item.component';

@Component({
  selector: 'app-game',
  standalone: true,
  imports: [CommonModule, FormsModule, GameItemComponent],
  templateUrl: './game.component.html',
  styleUrl: './game.component.css'
})

export class GameComponent{

  games: Game[] = [
    { title: 'Game 1', price: 10 },
    { title: 'Game 2', price: 50 },
    { title: 'Game 3', price: 300 }
  ];

  shouldPriceBeRed: boolean = false;
  search: string = 'Test';
  shouldShowComponent: boolean = true;

  changeColor(): void {
    this.shouldPriceBeRed = !this.shouldPriceBeRed;
  }

  searchText(): void {
    console.log(123);
  }

  deleteComponent(): void {
    this.shouldShowComponent = !this.shouldShowComponent;
  }
}

