using System;

namespace ToDoCore.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }

        public bool Completed { get; set; }

        public int Position { get; set; }

        public ToDoList ToDoList { get; set; }

        public Guid ListId { get; set; }

        public void Update(ToDoItem item)
        {
            Content = item.Content;
            Completed = item.Completed;
        }

        public void UpdatePosition(int position)
        {
            Position = position;
        }

        public ToDoItem(string content)
        {
            Content = content;
        }

        public ToDoItem()
        {
        }

        public ToDoItem(Guid listId)
        {
            ListId = listId;
        }
    }
}
