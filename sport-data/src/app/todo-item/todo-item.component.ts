import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Todo } from '../../interfaces/Todo';

@Component({
  selector: 'sd-todo-item',
  standalone: true,
  imports: [],
  templateUrl: './todo-item.component.html',
  styleUrl: './todo-item.component.css'
})
export class TodoItemComponent {
  @Input() todo!: Todo;

  @Output() changeCompleteState: EventEmitter<Todo> = new EventEmitter();
  // handleCompleteStateChange(todo: Todo): void {
  //   this.todo.isCompleted = !todo.isCompleted;
  // }
  handleCompleteStateChange(todo: Todo): void {
    this.changeCompleteState.emit(todo);
  }
}
