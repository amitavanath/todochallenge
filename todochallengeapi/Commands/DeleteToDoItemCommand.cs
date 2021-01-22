using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todochallengeapi.Commands
{
    public class DeleteToDoItemCommand : IRequest
    {
        [BindRequired]
        public int Id { get; set; }
       
    }
}
