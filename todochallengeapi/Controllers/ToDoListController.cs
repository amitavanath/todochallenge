using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using todochallengeapi.Commands;
using todochallengeapi.Entities;
using todochallengeapi.Models;
using todochallengeapi.Queries;
using todochallengeapi.Services;

namespace todochallengeapi.Controllers
{
    [ApiController]
    [Route("api/todolist")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public ToDoListController(IToDoListRepository toDoListRepository, IMapper mapper, IMediator mediator)
        {
            _todoListRepository = toDoListRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoListItems()
        {
            var query = new GetAllItemsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);

        }

        [HttpGet("{id}", Name="GetToDoListItem")]
        public async Task<IActionResult> GetToDoListItem(int id)
        {
            var query = new GetToDoItemByIdQuery(id);
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();


        }

        [HttpPost]
        public IActionResult CreateToDoListItem(CreateToDoItemCommand command)
        {
            var result = _mediator.Send(command);


            return CreatedAtRoute("GetToDoListItem",
                                    new {Id = result.Result},
                                    result);

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
        public async Task<IActionResult> DeleteToDoItem([FromRoute]DeleteToDoItemCommand command)
        {
            var result = await _mediator.Send(command);

            //var item = await _todoListRepository.GetToDoListItemAsync(id);
            
            //if( item == null)
            //{
            //    return NotFound();
            //}

            //_todoListRepository.DeleteToDoItem(item);

            return NoContent();
        }

    }
}