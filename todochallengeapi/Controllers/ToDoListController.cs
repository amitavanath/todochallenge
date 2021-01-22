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
        public IActionResult GetToDoListItems()
        {
            var todoListItems = _todoListRepository.GetToDoListItems();

            return Ok(todoListItems);
        }
    }
}