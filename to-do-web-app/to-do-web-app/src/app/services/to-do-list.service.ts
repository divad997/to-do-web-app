import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ToDoList } from '../models/to-do-list.model';
import { ToDoItem } from '../models/to-do-item.model';
import { isNgTemplate } from '@angular/compiler';

@Injectable({
    providedIn: 'root'
  })
export class ToDoListService {
    
    constructor(private http: HttpClient) { }

    getAllLists() {
        return this.http.get<Array<ToDoList>>(environment.apiUrl + '/to-do-lists');
    }

    getAllItemsFromList(id: string) {
        return this.http.get<Array<ToDoItem>>(environment.apiUrl + '/to-do-lists/' + id + '/to-do-items/');
    }

    getListById(id: string) {
        return this.http.get<ToDoList>(environment.apiUrl + '/to-do-lists/' + id);
    }

    createList(list: ToDoList) {
        return this.http.post(environment.apiUrl + '/to-do-lists', list);
    }  

    deleteListById(id: string) {
        return this.http.delete(environment.apiUrl + '/to-do-lists/' + id);
    }

    editList(list: ToDoList) {
        return this.http.put(environment.apiUrl + '/to-do-lists/', list);
    }
    
    createItem(listId: string, item: ToDoItem) {
        return this.http.post(environment.apiUrl + '/to-do-lists/' + listId + '/to-do-items/' , item); 
    }

    editItem(id: string, item: ToDoItem) {
        return this.http.put(environment.apiUrl + '/to-do-lists/' + id + '/to-do-items/' , item);
    }

    deleteItemById(listId: string, itemId: string) {
        return this.http.delete(environment.apiUrl + '/to-do-lists/' + listId + '/to-do-items/' + itemId);
    }
}
