using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace todochallengeapi.Commands
{
    public class DeleteToDoItemCommand : IRequest
    {
        [BindRequired]
        public Guid Id { get; set; }
       
    }
}
