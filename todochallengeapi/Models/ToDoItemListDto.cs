using System;

namespace todochallengeapi.Models
{
    public class ToDoItemListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public bool Completed { get; set;}
    }

    
}