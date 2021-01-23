using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace todochallengeapi.Commands
{
    public class DeleteToDoItemCommand : IRequest
    {
        [BindRequired]
        public int Id { get; set; }
       
    }
}
