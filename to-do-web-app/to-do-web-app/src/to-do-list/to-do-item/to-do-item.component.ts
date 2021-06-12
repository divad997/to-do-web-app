import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { async } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ToDoItem } from 'src/app/models/to-do-item.model';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { ToDoListDataService } from 'src/app/services/to-do-list-data.service';
import { ToDoListService } from 'src/app/services/to-do-list.service';

@Component({
  selector: 'app-to-do-item',
  templateUrl: './to-do-item.component.html',
  styleUrls: ['./to-do-item.component.css']
})
export class ToDoItemComponent implements OnInit, OnDestroy {
  @Input() item: ToDoItem;
  @Input() listId: string;
  @Output() itemChanged: EventEmitter<ToDoItem> = new EventEmitter();
  @Output() itemDeleted: EventEmitter<ToDoItem> = new EventEmitter();
  subscription: Subscription;

  constructor(private router: Router, private route: ActivatedRoute, private listService: ToDoListService, private toastrService: ToastrService, private dataService: ToDoListDataService) { }

  ngOnInit(): void {
    if (this.item === undefined)
      this.item = new ToDoItem();
    this.subscription = this.dataService.getMessage().subscribe(message => {
      if (message) {
        this.listId = message;
        this.createItem();
      }
    });
    this.isCreated();   
  }
  
  createItem() {
    this.listService.createItem(this.listId ,this.item).subscribe(
      (res: any) => {
        this.toastrService.success('Successful creation of item!');
        this.itemChanged.emit(this.item);
        this.item = new ToDoItem();
      },
      err => {
        this.toastrService.error(err);
      }
    );
  }

  createOrEditItem() {
    if (this.item.id === undefined && this.item.content !== undefined) {
      if (this.listId !== undefined) {
        this.createItem();
      } else {
        this.dataService.createList();
      }
    } else {
      this.listService.editItem(this.listId ,this.item).subscribe(
        (res: any) => {
          this.toastrService.success('Successful edit of item!');    
          this.itemChanged.emit(this.item);
        },
        err => {
          this.toastrService.error(err);
        }
      );
    }
  }
  
  deleteItem() {
    this.listService.deleteItemById(this.listId, this.item.id).subscribe(
      (res: any) => {
        this.toastrService.success('Successful removal of item!');    
        this.itemDeleted.emit(this.item);
      },
      err => {
        this.toastrService.error(err);
      }
    );
  }

  isCreated() {
    if (this.item.id === undefined)
      document.getElementById("deleteButton").style.visibility="hidden";
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
