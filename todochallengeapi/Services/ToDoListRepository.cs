using System.Collections.Generic;
using System.Threading.Tasks;
using todochallengeapi.Data;
using todochallengeapi.Entities;

namespace todochallengeapi.Services
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly IServiceContext _context;

        public ToDoListRepository(IServiceContext context) => _context = context
            ?? throw new System.ArgumentNullException(nameof(context));


        public async Task<IEnumerable<ToDoListItem>> GetToDoListItemsAsync()
        {
            return await _context.GetToDoListItems();
        }

        public async Task<ToDoListItem> GetToDoListItemAsync(int id)
        {
            return await _context.GetToDoListItem(id);
        }

        public void AddToDoItem(ToDoListItem item)
        {
            _context.AddToDoListItem(item);
        }

        public void UpdateToDoItemStatus(ToDoListItem item)
        {
            _context.UpdateToDoItemStatus(item);
        }
        
        public void DeleteToDoItem(ToDoListItem item)
        {
            _context.DeleteToDoItem(item);
        }
        
        
    }
}