using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using todochallengeapi.Models;
using todochallengeapi.Queries;
using todochallengeapi.Services;

namespace todochallengeapi.Handlers
{
    public class GetToDoItemByIdHandler : IRequestHandler<GetToDoItemByIdQuery, ToDoItemListDto>
    {

        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;
        public GetToDoItemByIdHandler(IToDoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<ToDoItemListDto> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
        {
            var todoListItem = await _todoListRepository.GetToDoListItemAsync(request.Id);

            if (todoListItem == null)
            {
                return null;
            }

            return _mapper.Map<ToDoItemListDto>(todoListItem);
        }
    }
}
