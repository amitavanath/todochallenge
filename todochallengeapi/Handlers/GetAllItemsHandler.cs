using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using todochallengeapi.Entities;
using todochallengeapi.Models;
using todochallengeapi.Queries;
using todochallengeapi.Services;

namespace todochallengeapi.Handlers
{
    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<ToDoItemListDto>>
    {
        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;
        public GetAllItemsHandler(IToDoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoItemListDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var todoListItems = await _todoListRepository.GetToDoListItemsAsync();
            return _mapper.Map<IEnumerable<ToDoListItem>, IEnumerable<ToDoItemListDto>>(todoListItems);
        }
    }
}
