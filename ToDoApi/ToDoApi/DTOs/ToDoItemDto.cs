using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.DTOs
{
    public class ToDoItemDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool Completed { get; set; }
    }
}
