import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Todo } from '../interfaces/Todo';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class TodoService {
  private cache: Todo[] = [];

  constructor(private httpClient: HttpClient) { }

  getTodos(): Observable<Todo[]> {
    return this.httpClient.get<Todo[]>('https://jsonplaceholder.typicode.com/todos');
    
    // if (!this.cache) {
    //   this.httpClient.get<Todo[]>('https://jsonplaceholder.typicode.com/todos').subscribe((todos) => this.cache = todos);
    //   console.log(this.cache);
    // }

    // return Promise.resolve(this.cache);
  }
}
