using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using todochallengeapi.Commands;
using todochallengeapi.Models;
using todochallengeapi.Services;

namespace todochallengeapi.Handlers
{
    public class UpdateToDoListItemStatusHandler : IRequestHandler<UpdateToDoListItemStatusCommand>
    {
        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;

        public UpdateToDoListItemStatusHandler(IToDoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateToDoListItemStatusCommand request, CancellationToken cancellationToken)
        {
            var item = await _todoListRepository.GetToDoListItemAsync(request.Id);

            var itemToUpdate = _mapper.Map<ToDoItemUpdationDto>(item);

            request.UpdateItem.ApplyTo(itemToUpdate);

            _mapper.Map(itemToUpdate, item);

            _todoListRepository.UpdateToDoItemStatus(item);

            return Unit.Value;
        }
    }
}
