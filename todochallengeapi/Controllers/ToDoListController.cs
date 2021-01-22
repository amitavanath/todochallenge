using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using todochallengeapi.Entities;
using todochallengeapi.Models;
using todochallengeapi.Services;

namespace todochallengeapi.Controllers
{
    [ApiController]
    [Route("api/todolist")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;

        public ToDoListController(IToDoListRepository toDoListRepository, IMapper mapper)
        {
            _todoListRepository = toDoListRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoListItems()
        {
            var todoListItems = await _todoListRepository.GetToDoListItemsAsync();

            return Ok(todoListItems);
        }

        [HttpGet("{id}", Name="GetToDoListItem")]
        public async Task<IActionResult> GetToDoListItem(int id)
        {
            var todoListItem = await _todoListRepository.GetToDoListItemAsync(id);

            if(todoListItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ToDoItemListDto>(todoListItem));
        }

        [HttpPost]
        public IActionResult CreateToDoListItem(ToDoItemCreationDto item)
        {
            var todoItemEntity = _mapper.Map<Entities.ToDoListItem>(item);
            _todoListRepository.AddToDoItem(todoItemEntity);

            var todoItemToReturn = _mapper.Map<ToDoItemListDto>(todoItemEntity);

            return CreatedAtRoute("GetToDoListItem",
                                    new {Id = todoItemToReturn.Id},
                                    todoItemToReturn);

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateToDoListItemStatus(int id, 
                                    JsonPatchDocument<ToDoItemUpdationDto> updateItem)
        {
            var item = await _todoListRepository.GetToDoListItemAsync(id);
            
            if( item == null)
            {
                return NotFound();
            }

            var itemToUpdate = _mapper.Map<ToDoItemUpdationDto>(item);

            updateItem.ApplyTo(itemToUpdate);

            _mapper.Map(itemToUpdate, item);

            _todoListRepository.UpdateToDoItemStatus(item);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var item = await _todoListRepository.GetToDoListItemAsync(id);
            
            if( item == null)
            {
                return NotFound();
            }

            _todoListRepository.DeleteToDoItem(item);

            return NoContent();
        }

    }
}