using MediatR;
using todochallengeapi.Entities;
using todochallengeapi.Models;

namespace todochallengeapi.Commands
{
    public class CreateToDoItemCommand : IRequest<ToDoItemListDto>
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}
