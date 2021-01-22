using System.Collections.Generic;
using System.Threading.Tasks;
using todochallengeapi.Entities;

namespace todochallengeapi.Data
{
    public interface IServiceContext
    {
        public Task<IEnumerable<ToDoListItem>> GetToDoListItems();
    }
}