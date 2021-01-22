using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using todochallengeapi.Models;

namespace todochallengeapi.Commands
{
    public class UpdateToDoListItemStatusCommand : IRequest
    {
        
        public int Id { get; set; }
        
       
        public JsonPatchDocument<ToDoItemUpdationDto> UpdateItem { get; set; }
    }
}
