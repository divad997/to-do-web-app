import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox'
import { DashboardComponent } from './dashboard/dashboard.component';
import { ToDoPreviewComponent } from './to-do-preview/to-do-preview.component';
import { ToDoListModule } from 'src/to-do-list/to-do-list.module';
import { DragDropModule } from '@angular/cdk/drag-drop';


@NgModule({
  declarations: [DashboardComponent, ToDoPreviewComponent],
  imports: [
    CommonModule,
    ToDoListModule,
    DragDropModule,
    MatCheckboxModule
  ],
  exports: [
    DashboardComponent
  ]
})
export class DashboardModule { }
