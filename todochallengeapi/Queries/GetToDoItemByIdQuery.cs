using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todochallengeapi.Models;

namespace todochallengeapi.Queries
{
    
    public class GetToDoItemByIdQuery : IRequest<ToDoItemListDto>
    {
        public Guid Id { get; set; }

        public GetToDoItemByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
