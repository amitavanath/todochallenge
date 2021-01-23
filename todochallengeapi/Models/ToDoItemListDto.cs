using System;

namespace todochallengeapi.Models
{
    public class ToDoItemListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public bool Status { get; set;}
        // public TaskStatus Status { get; set;}
    }

    
}