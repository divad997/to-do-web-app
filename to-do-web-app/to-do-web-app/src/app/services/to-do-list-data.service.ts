import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ToDoList } from '../models/to-do-list.model';
import { ToDoListService } from './to-do-list.service';

@Injectable({
  providedIn: 'root'
})
export class ToDoListDataService {
  constructor(private listService: ToDoListService, private toastrService: ToastrService) { }

  private subject = new Subject<any>();

  getMessage(): Observable<any> {
    return this.subject.asObservable();
  }
  
  createList() {
    const list = new ToDoList();
    this.listService.createList(list).subscribe(
      (res: ToDoList) => {
        this.subject.next(res.id);
      },
      err => {
        this.toastrService.error(err);
      }
    );
  }
}
