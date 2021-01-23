using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using todochallengeapi.Commands;
using todochallengeapi.Entities;
using todochallengeapi.Models;
using todochallengeapi.Services;

namespace todochallengeapi.Handlers
{
    public class CreateToDoItemHandler : IRequestHandler<CreateToDoItemCommand, ToDoItemListDto>
    {

        private readonly IToDoListRepository _todoListRepository;

        private readonly IMapper _mapper;
        public CreateToDoItemHandler(IToDoListRepository todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<ToDoItemListDto> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            ToDoListItem item = new ToDoListItem { Id = Guid.NewGuid(), Name = request.Name, Status = request.Status };

            var result = await _todoListRepository.AddToDoItemAsync(item);

            return _mapper.Map<ToDoListItem, ToDoItemListDto>(item);


        }
    }
}
