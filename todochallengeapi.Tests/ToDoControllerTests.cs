using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
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
using todochallengeapi.Data;
using todochallengeapi.Entities;
using todochallengeapi.Handlers;
using todochallengeapi.Models;
using todochallengeapi.Profiles;
using todochallengeapi.Queries;
using todochallengeapi.Services;
using Xunit;

namespace todochallengeapi.Tests
{
    public class ToDoControllerTests
    {
        private static IMapper _mapper;

        public ToDoControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ToDoItemProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void GetToDoItemsHandler_IsVerifiable_ReturnsNotNullResult()
        {
            //Arrange
            var _serviceContextMock = new Mock<IServiceContext>();
            _serviceContextMock.Setup(a => a.GetToDoListItemsAsync()).Returns(GetToDoListItemsFromFileAsync()).Verifiable();

            //Act
            IToDoListRepository _todoListRepository = new ToDoListRepository(_serviceContextMock.Object);

            GetAllItemsHandler handler = new GetAllItemsHandler(_todoListRepository, _mapper);

            GetAllItemsQuery query = new GetAllItemsQuery();
            
            //Act
            var result = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async void CreateItemsHandler_IsVerifiable_ReturnsNotNullResult()
        {
            //Arrange
            var _serviceContextMock = new Mock<IServiceContext>();
            _serviceContextMock.Setup(a => a.AddToDoListItemAsync(It.IsAny<ToDoListItem>())).Verifiable();

            CreateToDoItemCommand command = new CreateToDoItemCommand();
            command.Name = "Test";
            command.Completed = false;

            IToDoListRepository _todoListRepository = new ToDoListRepository(_serviceContextMock.Object);

            CreateToDoItemHandler handler = new CreateToDoItemHandler(_todoListRepository, _mapper);

            //Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.IsType<Guid>(result.Id);
            Assert.Matches(command.Name, result.Name);
            Assert.Equal(command.Completed, result.Completed);

            
        }


        [Fact]
        public async void DeleteItemHandler_IsVerifiable_ReturnsNotNullResult()
        {
            var itemToDelete = new ToDoListItem { Id = Guid.Parse("7a73db1a-f0a8-4937-a229-b9787ea2c54f"), Completed = true, Name = "test" };

            //Arrange
            var _serviceContextMock = new Mock<IServiceContext>();
            _serviceContextMock.Setup(a => a.DeleteToDoItem(It.IsAny<ToDoListItem>())).Verifiable();
            _serviceContextMock.Setup(a => a.GetToDoListItemAsync(itemToDelete.Id)).Verifiable();

            DeleteToDoItemCommand command = new DeleteToDoItemCommand();
            command.Id = Guid.NewGuid();

            IToDoListRepository _todoListRepository = new ToDoListRepository(_serviceContextMock.Object);

            DeleteToDoItemHandler handler = new DeleteToDoItemHandler(_todoListRepository, _mapper);

            //Act
            var _handler = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(handler);
        }


        [Fact]
        public async void UpdateToDoItemHandler_IsVerifiable_ReturnsNotNullResult()
        {
            //Arrange
            var itemToUpdate = new ToDoListItem { Id = Guid.Parse("7a73db1a-f0a8-4937-a229-b9787ea2c54f"), Completed = true, Name = "test" };

            var _serviceContextMock = new Mock<IServiceContext>();
            _serviceContextMock.Setup(a => a.UpdateToDoItemStatus(itemToUpdate)).Verifiable();
            _serviceContextMock.Setup(a => a.GetToDoListItemAsync(itemToUpdate.Id)).Returns(GetToDoListItemFromFileAsync(itemToUpdate.Id)).Verifiable();

            JsonPatchDocument<ToDoItemUpdationDto> patch = new JsonPatchDocument<ToDoItemUpdationDto>();
            patch.Replace(e => e.Completed, !itemToUpdate.Completed);

            var command = new UpdateToDoListItemStatusCommand();
            command.Id = itemToUpdate.Id;
            command.UpdateItem = patch;

            IToDoListRepository _todoListRepository = new ToDoListRepository(_serviceContextMock.Object);
            UpdateToDoListItemStatusHandler handler = new UpdateToDoListItemStatusHandler(_todoListRepository, _mapper);

            //Act
            var _handler = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert - verify able to call UpdateToDoListItemStatusHandler
            Assert.NotNull(handler);
        }

        public async Task<IEnumerable<ToDoListItem>> GetToDoListItemsFromFileAsync()
        {
            var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            return toDoListItems;
        }

        public async Task<ToDoListItem> GetToDoListItemFromFileAsync(Guid id)
        {
            var jsonData = await File.ReadAllTextAsync("ToDoItems.json");

            List<ToDoListItem> toDoListItems = new List<ToDoListItem>();
            toDoListItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(jsonData);

            return toDoListItems.Find(item => item.Id == id);
        }


    }
}
