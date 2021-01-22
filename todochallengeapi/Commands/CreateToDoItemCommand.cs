using MediatR;

namespace todochallengeapi.Commands
{
    public class CreateToDoItemCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
