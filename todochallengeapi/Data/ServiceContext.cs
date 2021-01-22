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

        public async Task<ToDoListItem> GetToDoListItem(int id)
        {
            return await GetToDoListItemFromFileAsync(id);
        }

        public void AddToDoListItem(ToDoListItem item)
        {
            AddToDoListItemsToFileAsync(item);
        }

        public void UpdateToDoItemStatus(ToDoListItem item)
        {
            UpdateToDoListItemStatusToFileAsync(item);
        }

        public void DeleteToDoItem(ToDoListItem item)
        {
            DeleteToDoListItemInFileAsync(item);
        }
        

        public async Task<IEnumerable<ToDoListItem>> GetToDoListItemsFromFileAsync()
        {
             var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            return toDoListItems;
        }

        public async Task<ToDoListItem> GetToDoListItemFromFileAsync(int id)
        {
             var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            return toDoListItems.Find(item => item.Id == id);
        }

        public async void AddToDoListItemsToFileAsync(ToDoListItem item)
        {
             var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            toDoListItems.Add(item);

            await File.WriteAllTextAsync("ToDoItems.json", JsonConvert.SerializeObject(toDoListItems));

        }

        public async void UpdateToDoListItemStatusToFileAsync(ToDoListItem todoItem)
        {

             var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            toDoListItems.Find(item => item.Id == todoItem.Id).Status = todoItem.Status;

            await File.WriteAllTextAsync("ToDoItems.json", JsonConvert.SerializeObject(toDoListItems));

        }

        public async void DeleteToDoListItemInFileAsync(ToDoListItem todoItem)
        {

             var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            int result = toDoListItems.RemoveAll(x => x.Id == todoItem.Id);

            await File.WriteAllTextAsync("ToDoItems.json", JsonConvert.SerializeObject(toDoListItems));

        }
    }
}