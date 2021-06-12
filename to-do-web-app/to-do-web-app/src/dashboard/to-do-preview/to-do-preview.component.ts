import { Component, Input, OnInit, Output } from '@angular/core';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { ToastrService } from 'ngx-toastr';
import { ToDoListService } from 'src/app/services/to-do-list.service';
import { EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-to-do-preview',
  templateUrl: './to-do-preview.component.html',
  styleUrls: ['./to-do-preview.component.css']
})
export class ToDoPreviewComponent implements OnInit {  
  @Input() list: ToDoList;
  @Output() deleteChange: EventEmitter<string> = new EventEmitter();

  constructor(private listService: ToDoListService, private toastrService: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

  onDelete() {
    this.listService.deleteListById(this.list.id).subscribe(
      (res: any) => {
        this.toastrService.success(res, 'Successful deletion of list!');
        this.deleteChange.emit(this.list.id);
      },
      err => {
        this.toastrService.error(err);
      }
    );
  }

  onEdit() {
    this.router.navigate(['/to-do-list/', this.list.id]);
  }

  get completed() {
    return this.list.itemsList.filter(x => x.completed == true);
  }

  get notCompleted() {
    return this.list.itemsList.filter(x => x.completed == false);
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.list.itemsList, event.previousIndex, event.currentIndex);
  }
}
