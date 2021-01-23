using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using todochallengeapi.Commands;
using todochallengeapi.Services;

namespace todochallengeapi.Handlers
{
    public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItemCommand>
    {

        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;
        public DeleteToDoItemHandler(IToDoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _todoListRepository.GetToDoListItemAsync(request.Id);

            _todoListRepository.DeleteToDoItem(item);

            return (Unit.Value);

        }
    }
}
