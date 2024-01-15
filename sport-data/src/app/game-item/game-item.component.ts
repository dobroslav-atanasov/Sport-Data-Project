import { CommonModule } from '@angular/common';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-game-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './game-item.component.html',
  styleUrl: './game-item.component.css'
})

export class GameItemComponent implements OnInit, OnDestroy {
  ngOnDestroy(): void {
    console.log('destroy')
  }
  ngOnInit(): void {
    console.log('init');
  }

  @Input() currentGame!: Game;

}

export interface Game {
  title: string,
  price: number,
}