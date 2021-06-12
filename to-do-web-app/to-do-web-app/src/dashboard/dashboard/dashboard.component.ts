import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ToDoList } from 'src/app/models/to-do-list.model';
import { ToDoListService } from 'src/app/services/to-do-list.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  allLists: Array<ToDoList>;

  constructor(private listService: ToDoListService, private toastrService: ToastrService, private router: Router) { 
    this.loadAllLists();
  }

  ngOnInit(): void {
  }

  loadAllLists() {  
    this.listService.getAllLists().subscribe(
      (res: any) => {
        this.toastrService.success(res, 'Successful retrieval of lists!');
        this.allLists = res as Array<ToDoList>;
      },
      err => {
        this.toastrService.error(err);
      }
    );
  }

  deleteChange($event) {
      this.allLists = this.allLists.filter(list => list.id !== $event);
  }

  onAdd() {
    this.router.navigate(['/to-do-list/']);
  }
  
}
