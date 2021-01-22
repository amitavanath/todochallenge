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


        public async Task<IEnumerable<ToDoListItem>> GetToDoListItems()
        {
            return await _context.GetToDoListItems();
        }
        
    }
}