using AutoMapper;
using System.Collections.Generic;

namespace todochallengeapi.Profiles
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<Entities.ToDoListItem, Models.ToDoItemListDto>();

            CreateMap<Entities.ToDoListItem, Models.ToDoItemUpdationDto>();

            CreateMap<Models.ToDoItemUpdationDto, Entities.ToDoListItem>();

            CreateMap<List<Entities.ToDoListItem>, List<Models.ToDoItemListDto>>();
        }
    }
}