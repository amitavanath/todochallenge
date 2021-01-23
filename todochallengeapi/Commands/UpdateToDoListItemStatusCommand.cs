using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using todochallengeapi.Models;

namespace todochallengeapi.Commands
{
    public class UpdateToDoListItemStatusCommand : IRequest
    {
        
        public Guid Id { get; set; }
        
       
        public JsonPatchDocument<ToDoItemUpdationDto> UpdateItem { get; set; }
    }
}
