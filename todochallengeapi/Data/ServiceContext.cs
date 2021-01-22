using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using todochallengeapi.Entities;

namespace todochallengeapi.Data
{
    public class ServiceContext : IServiceContext
    {
        private readonly IConfiguration _config;

        public ServiceContext(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<ToDoListItem>> GetToDoListItems()
        {
            return await GetToDoListItemsFromFileAsync();
        }

        public async Task<IEnumerable<ToDoListItem>> GetToDoListItemsFromFileAsync()
        {
             var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            return toDoListItems;
        }
    }
}