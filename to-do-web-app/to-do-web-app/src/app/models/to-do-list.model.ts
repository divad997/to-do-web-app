import { ToDoItem } from './to-do-item.model';

export class ToDoList {
    id: string;
    title: string;
    dueDate: Date;
    itemsList: Array<ToDoItem> = new Array<ToDoItem>();
}
