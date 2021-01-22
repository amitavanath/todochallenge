using System.Collections.Generic;
using System.Threading.Tasks;
using todochallengeapi.Entities;

namespace todochallengeapi.Services
{
    public interface IToDoListRepository
    {
        public Task<IEnumerable<ToDoListItem>> GetToDoListItemsAsync();

        public Task<ToDoListItem> GetToDoListItemAsync(int id);

        public Task<int> AddToDoItemAsync(ToDoListItem item);

        public void UpdateToDoItemStatus(ToDoListItem item);

        public void DeleteToDoItem(ToDoListItem item);
    }
}