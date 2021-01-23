using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using todochallengeapi.Entities;

namespace todochallengeapi.Data
{
    public interface IServiceContext
    {
        public Task<IEnumerable<ToDoListItem>> GetToDoListItemsAsync();

        public Task<ToDoListItem> GetToDoListItemAsync(Guid id);

        public Task<ToDoListItem> AddToDoListItemAsync(ToDoListItem item);

        public void UpdateToDoItemStatus(ToDoListItem item);

        public void DeleteToDoItem(ToDoListItem item);
    }
}