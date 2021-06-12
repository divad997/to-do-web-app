using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.DTOs
{
    public class ToDoListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public List<ToDoItemDto> ItemsList { get; set; }
    }
}
