import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from 'src/dashboard/dashboard/dashboard.component';
import { ToDoListComponent } from 'src/to-do-list/to-do-list/to-do-list.component';



const routes: Routes = [
  { 
    path: "",  
    redirectTo: "/dashboard",
    pathMatch: "full",
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'to-do-list/:id',
    component: ToDoListComponent
  },
  {
    path: 'to-do-list',
    component: ToDoListComponent
  }, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
