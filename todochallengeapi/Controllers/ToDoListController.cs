using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todochallengeapi.Services;

namespace todochallengeapi.Controllers
{
    [ApiController]
    [Route("api/todolist")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _todoListRepository;

        public ToDoListController(IToDoListRepository toDoListRepository)
        {
            _todoListRepository = toDoListRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoListItems()
        {
            var todoListItems = await _todoListRepository.GetToDoListItemsAsync();

            return Ok(todoListItems);
        }
    }
}