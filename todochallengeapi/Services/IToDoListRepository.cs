using System.Collections.Generic;
using System.Threading.Tasks;
using todochallengeapi.Entities;

namespace todochallengeapi.Services
{
    public interface IToDoListRepository
    {
        public Task<IEnumerable<ToDoListItem>> GetToDoListItemsAsync();
    }
}