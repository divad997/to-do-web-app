using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;
using ToDoCore.Models;
using ToDoInfrastructure;

namespace ToDoApi.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;

        public ToDoListService(ToDoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ToDoItemDto>> GetAllItemsFromListAsync(Guid listId)
        {
            var list = await _context.ToDoLists.FindAsync(listId);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                var items = await _context.ToDoItems
                .Where(i => i.ListId == listId)
                .OrderBy(i => i.Position)
                .ToListAsync();
                var dtoItems = _mapper.Map<List<ToDoItem>, List<ToDoItemDto>>(items);

                if (items == null)
                    throw new Exception("Items from the given list could not be retrieved!");
                else
                    return dtoItems;
            }  
        } 

        public async Task<ToDoItemDto> GetToDoItemByIdAsync(Guid listId, Guid itemId)
        {
           
            var list = await _context.ToDoLists
                    .Include(l => l.ItemsList)
                    .FirstOrDefaultAsync(l => l.Id == listId);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                var item = list.ItemsList.Find(i => i.Id == itemId);
                var dtoItem = _mapper.Map(item, typeof(ToDoList), typeof(ToDoListDto)); 

                if (item == null)
                    throw new Exception("Item with given id doesn't exist!");
                else
                {
                    return (ToDoItemDto)dtoItem;
                }
            }
        }
        public async Task<ToDoItemDto> CreateToDoItemAsync(Guid listId, ToDoItemDto item)
        {
            var list = await _context.ToDoLists
                    .Include(l => l.ItemsList)
                    .FirstOrDefaultAsync(l => l.Id == listId);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                ToDoItem newItem = new ToDoItem(listId); 
                _mapper.Map(item, newItem);
                newItem.UpdatePosition(list.ItemsList.Count);

                _context.ToDoItems.Add(newItem);
                await _context.SaveChangesAsync();

                var dtoItem = _mapper.Map(newItem, typeof(ToDoItem), typeof(ToDoItemDto));
                return (ToDoItemDto)dtoItem;
            }
        }

        public async Task<bool> EditToDoItemAsync(Guid listId, ToDoItemDto item)
        {
            var list = await _context.ToDoLists
                .Include(l => l.ItemsList)
                .FirstOrDefaultAsync(l => l.Id == listId);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                var editedItem = list.ItemsList.Find(i => i.Id == item.Id);

                if (editedItem == null)
                    throw new Exception("Item with given id doesn't exist!");
                else
                {
                    var modelItem = _mapper.Map(item, typeof(ToDoItemDto), typeof(ToDoItem));
                    editedItem.Update((ToDoItem)modelItem);

                    var saved = await _context.SaveChangesAsync();

                    return saved == 1;
                } 
            }           
        }

        public async Task<bool> UpdateToDoItemPositionAsync(Guid listId, Guid itemId, int newPosition)
        {
            var list = await _context.ToDoLists
                .Include(l => l.ItemsList)
                .FirstOrDefaultAsync(l => l.Id == listId);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                var toDoItems = list.ItemsList.OrderBy(i => i.Position).ToList();

                var editedItem = toDoItems.FirstOrDefault(i => i.Id == itemId);
                var oldPosition = editedItem.Position;

                toDoItems[oldPosition] = toDoItems[newPosition];
                toDoItems[newPosition] = editedItem;

                if (oldPosition < newPosition)
                    for (int i = oldPosition; i <= newPosition; i++)
                        toDoItems[i].UpdatePosition(i);

                else if (oldPosition > newPosition)
                    for (int i = newPosition; i <= oldPosition; i++)
                        toDoItems[i].UpdatePosition(i);

                return await _context.SaveChangesAsync() == Math.Abs(oldPosition - newPosition) + 1;
            }      
        }

        public async Task<bool> DeleteToDoItemAsync(Guid listId, Guid itemId)
        {
            var list = await _context.ToDoLists
                .Include(l => l.ItemsList)
                .FirstOrDefaultAsync(l => l.Id == listId);

            bool removed = false;

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                var item = list.ItemsList.Find(i => i.Id == itemId);
                if (item == null)
                    throw new Exception("Item with given id doesn't exist");
                else
                    removed = list.ItemsList.Remove(item);
            }
                
            await _context.SaveChangesAsync();

            return removed;
        }

        public async Task<List<ToDoListDto>> GetAllListsAsync()
        {
            var lists = await _context.ToDoLists
                .Include(l => l.ItemsList.OrderBy(i => i.Position))
                .OrderByDescending(l => l.Position)
                .ToListAsync();

            var dtoLists = _mapper.Map<List<ToDoList>, List<ToDoListDto>>(lists);

            if (lists == null)
                throw new Exception("Couldn't retrieve the lists!");
            else
                return dtoLists;
        }

        public async Task<ToDoListDto> GetToDoListByIdAsync(Guid id)
        {
            var list = await _context.ToDoLists
                .Include(l => l.ItemsList.OrderBy(i => i.Position))
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                var dtoList = _mapper.Map(list, typeof(ToDoList), typeof(ToDoListDto));
                return (ToDoListDto)dtoList;
            }
        }

        public async Task<List<ToDoListDto>> SearchToDoListsByTitleAsync(string tag)
        {
            var lists = await _context.ToDoLists
                .Where(l => l.Title.ToLower().StartsWith(tag.ToLower()))
                .Include(l => l.ItemsList.OrderBy(i => i.Position))
                .ToListAsync();

            if (lists == null)
                throw new Exception("Couldn't retrieve the lists!");
            else
            {
                var dtoLists = _mapper.Map<List<ToDoList>, List<ToDoListDto>>(lists);
                return dtoLists;
            }      
        }

        public async Task<ToDoListDto> CreateToDoListAsync(ToDoListDto list)
        {
            if (_context.ToDoLists == null)
                throw new Exception("Couldn't retrieve the lists!");
            else
            {
                ToDoList newList = new ToDoList();
                _mapper.Map(list, newList);
                newList.UpdatePosition(_context.ToDoLists.Count());

                _context.ToDoLists.Add(newList);
                await _context.SaveChangesAsync();

                var dtoList = _mapper.Map(newList, typeof(ToDoList), typeof(ToDoListDto));
                return (ToDoListDto)dtoList;
            }
        }

        public async Task<bool> EditToDoListAsync(ToDoListDto list)
        {
            var editedList = await _context.ToDoLists.FindAsync(list.Id);

            var modelList = _mapper.Map(list, typeof(ToDoListDto), typeof(ToDoList));
            editedList.Update((ToDoList)modelList);
 
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateToDoListPositionAsync(Guid listId, int newPosition)
        {
            var toDoLists = await _context.ToDoLists.OrderBy(l => l.Position).ToListAsync();

            var editedList = toDoLists.Find(l => l.Id == listId);
            var oldPosition = editedList.Position;

            toDoLists.Remove(editedList);
            toDoLists.Insert(newPosition, editedList);

            if (oldPosition < newPosition)
                for (int i = oldPosition; i <= newPosition; i++)
                    toDoLists[i].UpdatePosition(i);

            else if (oldPosition > newPosition)
                for (int i = newPosition; i <= oldPosition; i++)
                    toDoLists[i].UpdatePosition(i);

            return await _context.SaveChangesAsync() == Math.Abs(oldPosition - newPosition) + 1;
        }

        public async Task<bool> DeleteToDoListAsync(Guid listId)
        {
            var list = await _context.ToDoLists.FindAsync(listId);

            if (list == null)
                throw new Exception("List with given id doesn't exist!");
            else
            {
                _context.ToDoLists.Remove(list);
                return await _context.SaveChangesAsync() == 1;
            }
        }
    }
}
