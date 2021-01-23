using MediatR;
using todochallengeapi.Entities;
using todochallengeapi.Models;

namespace todochallengeapi.Commands
{
    public class CreateToDoItemCommand : IRequest<ToDoItemListDto>
    {   
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}
