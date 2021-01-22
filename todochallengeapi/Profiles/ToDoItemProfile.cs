using AutoMapper;

namespace todochallengeapi.Profiles
{
    public class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<Entities.ToDoListItem, Models.ToDoItemListDto>();

            CreateMap<Models.ToDoItemCreationDto, Entities.ToDoListItem>();

            CreateMap<Entities.ToDoListItem, Models.ToDoItemUpdationDto>();

            CreateMap<Models.ToDoItemUpdationDto, Entities.ToDoListItem>();
        }
    }
}