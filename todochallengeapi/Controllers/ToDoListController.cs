using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using todochallengeapi.Commands;
using todochallengeapi.Models;
using todochallengeapi.Queries;
using todochallengeapi.Services;

namespace todochallengeapi.Controllers
{
    [EnableCors("Policy")]
    [ApiController]
    [Route("api/todolist")]
    public class ToDoListController : ControllerBase
    {
        

        private readonly IMediator _mediator;

        public ToDoListController(IMediator mediator) => _mediator = mediator
            ?? throw new System.ArgumentNullException(nameof(mediator));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemListDto>>> GetToDoListItems()
        {
            var query = new GetAllItemsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);

        }

        [HttpGet("{id}", Name="GetToDoListItem")]
        public async Task<ActionResult<IEnumerable<ToDoItemListDto>>> GetToDoListItem(Guid id)
        {
            var query = new GetToDoItemByIdQuery(id);
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();


        }

        
        [HttpPost]
        public async Task<ActionResult<ToDoItemListDto>> CreateToDoListItem(CreateToDoItemCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem([FromRoute]DeleteToDoItemCommand command)
        {
            var result = await _mediator.Send(command);

            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateToDoListItemStatus(Guid id, JsonPatchDocument<ToDoItemUpdationDto> item)
        {
            var result = await _mediator.Send(new UpdateToDoListItemStatusCommand { 
                                                        Id = id, UpdateItem = item});
        
            return NoContent();
        }


        [HttpOptions]
        public IActionResult GetToDoOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PATCH");
            return Ok();
        }

    }
}