using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoCore.Models
{
    public class ToDoList
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Reminded { get; set; }
        public int Position { get; set; }
        public List<ToDoItem> ItemsList { get; set; }       

        public void Update(ToDoList list)
        {
            Title = list.Title;
            DueDate = list.DueDate;
        }
        
        public void UpdatePosition(int position)
        {
            Position = position;
        }

        public ToDoList(string title, DateTime dueDate)
        {
            Title = title;
            DueDate = dueDate;
            Position = 0;
            ItemsList = new List<ToDoItem>();
        }

        public ToDoList()
        {
            ItemsList = new List<ToDoItem>();
        }
    }
}
