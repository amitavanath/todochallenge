using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todochallengeapi.Commands;
using todochallengeapi.Controllers;
using todochallengeapi.Entities;
using todochallengeapi.Models;
using todochallengeapi.Queries;
using Xunit;

namespace todochallengeapi.Tests
{
    public class ToDoControllerTests
    {
        [Fact]
        public async void GetToDoListItems_ReturnsValue()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            GetAllItemsQuery query = new GetAllItemsQuery();

            var items = await GetToDoListItemsFromFileAsync();
            
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetAllItemsQuery>(), new System.Threading.CancellationToken())).Returns(Task.FromResult(items));

            var controller = new ToDoListController(mediator.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //ACT 
            var result = await controller.GetToDoListItems();

            //ASSERT
            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public async void GetToDoListItems_NoResults_ReturnsOK()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            GetAllItemsQuery query = new GetAllItemsQuery();

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetAllItemsQuery>(), new System.Threading.CancellationToken()));

            var controller = new ToDoListController(mediator.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //ACT 
            var result = await controller.GetToDoListItems();

            //ASSERT
            Assert.IsType<OkObjectResult>(result.Result);

        }


        [Fact]
        public void CreateToDoListItem_ReturnsOKResult()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            CreateToDoItemCommand command = new CreateToDoItemCommand();
            command.Name = "Test";
            command.Completed = false;

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CreateToDoItemCommand>(), new System.Threading.CancellationToken()));

            var controller = new ToDoListController(mediator.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //ACT 
            var result = controller.CreateToDoListItem(command);

            //ASSERT
            Assert.IsType<ActionResult<ToDoItemListDto>>(result.Result);
            Assert.Null(result.Result.Value);

        }

        [Fact]
        public async void CreateToDoListItem_ReturnsValidDto_ReturnsOKResult()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            var items = await GetToDoListItemsFromFileAsync();

            CreateToDoItemCommand command = new CreateToDoItemCommand();
            command.Name = "Test";
            command.Completed = false;

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CreateToDoItemCommand>(), new System.Threading.CancellationToken()))
                        .Returns(Task.FromResult(items.First()));

            var controller = new ToDoListController(mediator.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //ACT 
            var result = await controller.CreateToDoListItem(command);

            //ASSERT
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result.Result);

        }


        [Fact]
        public async void DeleteToDoListItem_ReturnsNoContentResult()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();

            DeleteToDoItemCommand command = new DeleteToDoItemCommand();

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<DeleteToDoItemCommand>(), new System.Threading.CancellationToken()));

            var controller = new ToDoListController(mediator.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //ACT 
            var result = await controller.DeleteToDoItem(command);

            //ASSERT
            Assert.IsType<NoContentResult>(result);

        }


        [Fact]
        public async void UpdateToDoListItemStatus_ReturnsNoContentResult()
        {
            //Arrange
            var itemToUpdate = new ToDoListItem { Id = Guid.Parse("7a73db1a-f0a8-4937-a229-b9787ea2c54f"), Completed = true, Name = "test" };

            var httpContext = new DefaultHttpContext();

            var items = await GetToDoListItemsFromFileAsync();

            JsonPatchDocument<ToDoItemUpdationDto> patch = new JsonPatchDocument<ToDoItemUpdationDto>();
            patch.Replace(e => e.Completed, !itemToUpdate.Completed);

            UpdateToDoListItemStatusCommand command = new UpdateToDoListItemStatusCommand();
            command.Id = itemToUpdate.Id;
            command.UpdateItem = patch;

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<UpdateToDoListItemStatusCommand>(), new System.Threading.CancellationToken()));

            var controller = new ToDoListController(mediator.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            //ACT 
            var result = await controller.UpdateToDoListItemStatus(command.Id, command.UpdateItem);

            //ASSERT
            Assert.IsType<NoContentResult>(result);

        }

        public async Task<IEnumerable<ToDoItemListDto>> GetToDoListItemsFromFileAsync()
        {
            var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoItemListDto> toDoListItems = new List<ToDoItemListDto>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoItemListDto>>(jsonData);

            return toDoListItems;
        }
    }

}
