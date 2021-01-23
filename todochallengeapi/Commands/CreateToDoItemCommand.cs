using MediatR;
using todochallengeapi.Entities;

namespace todochallengeapi.Commands
{
    public class CreateToDoItemCommand : IRequest<ToDoListItem>
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
