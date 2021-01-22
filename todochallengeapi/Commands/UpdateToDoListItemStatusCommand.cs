using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todochallengeapi.Models;

namespace todochallengeapi.Commands
{
    public class UpdateToDoListItemStatusCommand : IRequest
    {
        [FromQuery]
        public int Id { get; set; }
        
        [FromBody]
        public JsonPatchDocument<ToDoItemUpdationDto> UpdateItem { get; set; }
    }
}
