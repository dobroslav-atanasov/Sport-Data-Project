import { Component, OnInit } from '@angular/core';
import { Todo } from '../../interfaces/Todo';
import { CommonModule } from '@angular/common';
import { TodoItemComponent } from '../todo-item/todo-item.component';
import { TodoService } from '../../services/todo.service';

@Component({
  selector: 'sd-todo',
  standalone: true,
  imports: [CommonModule, TodoItemComponent],
  providers: [TodoService],
  templateUrl: './todo.component.html',
  styleUrl: './todo.component.css'
})
export class TodoComponent implements OnInit {
  todos: Todo[] = [];

  constructor(private todosService: TodoService) {
  }

  ngOnInit(): void {
    this.todosService.getTodos().subscribe(data =>{
      this.todos = data;
    })
    
  }
  // todos: Todo[] = [
  //   {
  //     title: 'Todo 1', isCompleted: false
  //   },
  //   {
  //     title: 'Todo 2', isCompleted: true
  //   },
  //   {
  //     title: 'Todo 3', isCompleted: false
  //   },
  //   {
  //     title: 'Todo 4', isCompleted: true
  //   },
  // ];

  changeAllToCompleted(): void {
    for (const todo of this.todos) {
      todo.completed = true;
    }
  };

  handleStateChange(todo: Todo): void {
    todo.completed = !todo.completed;
  }
}
