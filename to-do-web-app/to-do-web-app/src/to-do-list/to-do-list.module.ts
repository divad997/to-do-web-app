import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToDoListComponent } from './to-do-list/to-do-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToDoItemComponent } from './to-do-item/to-do-item.component';

@NgModule({
  declarations: [ToDoListComponent, ToDoItemComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    ToDoListComponent
  ]
})
export class ToDoListModule { }
