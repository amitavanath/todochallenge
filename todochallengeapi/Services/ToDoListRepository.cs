using System;
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
            return await _context.GetToDoListItemsAsync();
        }

        public async Task<ToDoListItem> GetToDoListItemAsync(Guid id)
        {
            return await _context.GetToDoListItemAsync(id);
        }

        public async Task<ToDoListItem> AddToDoItemAsync(ToDoListItem item)
        {
            return await _context.AddToDoListItemAsync(item);
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