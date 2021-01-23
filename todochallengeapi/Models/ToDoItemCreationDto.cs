using System;

namespace todochallengeapi.Models
{
    public class ToDoItemCreationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set;}

     
    }

}