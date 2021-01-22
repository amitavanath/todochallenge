using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todochallengeapi.Models;

namespace todochallengeapi.Queries
{
    public class GetAllItemsQuery : IRequest<IEnumerable<ToDoItemListDto>>
    {

    }
}
