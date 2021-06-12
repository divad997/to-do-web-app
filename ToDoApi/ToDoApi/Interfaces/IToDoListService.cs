using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.DTOs;
using ToDoCore.Models;

namespace ToDoApi.Interfaces
{
    public interface IToDoListService
    {
        Task<List<ToDoItemDto>> GetAllItemsFromListAsync(Guid listId);
        Task<ToDoItemDto> GetToDoItemByIdAsync(Guid listId, Guid itemId);
        Task<ToDoItemDto> CreateToDoItemAsync(Guid listId, ToDoItemDto item);
        Task<bool> EditToDoItemAsync(Guid listId, ToDoItemDto item);
        Task<bool> DeleteToDoItemAsync(Guid listId, Guid itemId);
        Task<List<ToDoListDto>> GetAllListsAsync();
        Task<ToDoListDto> GetToDoListByIdAsync(Guid id);
        Task<List<ToDoListDto>> SearchToDoListsByTitleAsync(string tag);
        Task<ToDoListDto> CreateToDoListAsync(ToDoListDto list);
        Task<bool> EditToDoListAsync(ToDoListDto list);
        Task<bool> DeleteToDoListAsync(Guid listId);
        Task<bool> UpdateToDoListPositionAsync(Guid listId, int newPosition);
        Task<bool> UpdateToDoItemPositionAsync(Guid listId, Guid itemId, int newPosition);
    }
}
