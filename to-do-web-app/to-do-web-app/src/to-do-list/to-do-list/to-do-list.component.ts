import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ToDoItem } from 'src/app/models/to-do-item.model';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { ToDoListDataService } from 'src/app/services/to-do-list-data.service';
import { ToDoListService } from 'src/app/services/to-do-list.service';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent implements OnInit, OnDestroy {
  listId: string;
  list: ToDoList = new ToDoList();
  subscription: Subscription;

  constructor(private router: Router, private route: ActivatedRoute, private listService: ToDoListService, private toastrService: ToastrService, private dataService: ToDoListDataService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => { this.listId = params.id });
    this.loadList();
    this.subscription = this.dataService.getMessage().subscribe(message => {
      if (message) {
        this.list.id = message;
        this.listId = message;
      }
    });
  }

  loadList() {  
    if (this.listId !== undefined) {
      this.listService.getListById(this.listId).subscribe(
        (res: any) => {
          this.toastrService.success(res, 'Successful retrieval of list by id!');
          this.list = res as ToDoList;
        },
        err => {
          this.toastrService.error(err);
        }
      );
    } else {
      this.list = new ToDoList();
    }
  }

  createOrEditList() {
    if (this.listId !== undefined) {
      this.listService.editList(this.list).subscribe(
        (res: any) => {
          this.toastrService.success('Successful edit of list!');
        },
        err => {
          this.toastrService.error(err);
         // this.loadList();
        }
      );
    } else {
      this.listService.createList(this.list).subscribe(
        (res: ToDoList) => {
          this.toastrService.success(res.title, 'Successful creation of list!');
        },
        err => {
          this.toastrService.error(err);
        }
      );
    }
  }

  itemChanged(item: ToDoItem) {
    this.listService.getAllItemsFromList(this.list.id).subscribe(
      (res: Array<ToDoItem>) => {
        this.toastrService.success('Successful retrieval of items from list!');
        this.list.itemsList = res;
        //this.reloadComponent();
      },
      err => {
        this.toastrService.error(err);
      }
    );
  }

  itemDeleted(item: ToDoItem) {
    this.list.itemsList = this.list.itemsList.filter(x => x.id !== item.id);
  }

  toDashboard() { 
    if (this.listId !== undefined)
      this.router.navigate(['../../'], { relativeTo: this.route });
    else
      this.router.navigate(['../'], { relativeTo: this.route });
  }

  get completed() {
    return this.list.itemsList.filter(x => x.completed == true);
  }

  get notCompleted() {
    return this.list.itemsList.filter(x => x.completed == false);
  }

  reloadComponent() {
    let currentUrl = this.router.url;
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([currentUrl]);
    }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
