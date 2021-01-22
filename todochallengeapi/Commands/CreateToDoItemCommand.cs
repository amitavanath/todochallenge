using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todochallengeapi.Models;

namespace todochallengeapi.Commands
{
    public class CreateToDoItemCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
