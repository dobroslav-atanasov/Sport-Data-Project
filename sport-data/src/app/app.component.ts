import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { GameComponent } from './game/game.component';
import { TodoComponent } from './todo/todo.component';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'sd-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, GameComponent, TodoComponent],
  providers: [TodoService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'sport-data';
}
