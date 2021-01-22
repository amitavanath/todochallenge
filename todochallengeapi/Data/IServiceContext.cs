using System.Collections.Generic;
using System.Threading.Tasks;
using todochallengeapi.Entities;

namespace todochallengeapi.Data
{
    public interface IServiceContext
    {
        public Task<IEnumerable<ToDoListItem>> GetToDoListItems();

        public Task<ToDoListItem> GetToDoListItem(int id);

        public Task<int> AddToDoListItemAsync(ToDoListItem item);

        public void UpdateToDoItemStatus(ToDoListItem item);

        public void DeleteToDoItem(ToDoListItem item);
    }
}