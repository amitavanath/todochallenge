using System;

namespace todochallengeapi.Models
{
    public class ToDoItemCreationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set;}

     
    }

}