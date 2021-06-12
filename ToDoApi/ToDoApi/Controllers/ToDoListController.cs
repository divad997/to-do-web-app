using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ToDoApi.DTOs;
using ToDoApi.Interfaces;

namespace ToDoApi.Controllers
{
    [Route("api/to-do-lists")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet("{listId}/to-do-items/")]        
        public async Task<IActionResult> GetAllItemsFromList(Guid listId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemsList = await _toDoListService.GetAllItemsFromListAsync(listId);

            if (itemsList == null)
            {
                return NotFound();
            }

            return Ok(itemsList);
        }

        [HttpGet("{listId}/to-do-items/{itemId}")]
        public async Task<IActionResult> GetToDoItemById(Guid listId, Guid itemId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _toDoListService.GetToDoItemByIdAsync(listId, itemId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost("{listId}/to-do-items")]
        public async Task<IActionResult> CreateToDoItem(Guid listId, [FromBody] ToDoItemDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = await _toDoListService.CreateToDoItemAsync(listId, item);

            return CreatedAtAction("GetToDoItemById", new { listId = listId, itemId = newItem.Id }, newItem);
        }

        [HttpPut("{listId}/to-do-items")]
        public async Task<IActionResult> EditToDoItem(Guid listId, [FromBody] ToDoItemDto item)
        {
            if (await _toDoListService.EditToDoItemAsync(listId, item) == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{listId}/to-do-items/{itemId}")]
        public async Task<IActionResult> UpdateToDoItemPosition(Guid listId, Guid itemId, [FromBody] int position)
        {
            if (await _toDoListService.UpdateToDoItemPositionAsync(listId, itemId, position) == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{listId}/to-do-items/{itemId}")]
        public async Task<IActionResult> DeleteToDoItem(Guid listId, Guid itemId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _toDoListService.DeleteToDoItemAsync(listId, itemId) == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLists()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lists = await _toDoListService.GetAllListsAsync();

            if (lists == null)
            {
                return NotFound();
            }

            return Ok(lists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoListById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var list = await _toDoListService.GetToDoListByIdAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
        
        [HttpGet("bytitle/{tag}")]
        public async Task<IActionResult> SearchToDoListsByTitle(string tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lists = await _toDoListService.SearchToDoListsByTitleAsync(tag);

            if (lists == null)
            {
                return NotFound();
            }
                
            return Ok(lists);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] ToDoListDto list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = await _toDoListService.CreateToDoListAsync(list);

            return CreatedAtAction("GetToDoListById", new { id = newItem.Id }, newItem);
        }

        [HttpPut]
        public async Task<IActionResult> EditToDoList([FromBody] ToDoListDto list)
        {
            if (await _toDoListService.EditToDoListAsync(list) == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{listId}")]
        public async Task<IActionResult> UpdateToDoListPosition(Guid listId, [FromBody] int position)
        {
            if (await _toDoListService.UpdateToDoListPositionAsync(listId, position) == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _toDoListService.DeleteToDoListAsync(id) == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
