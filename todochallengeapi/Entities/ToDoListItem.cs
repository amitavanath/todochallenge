using System;

namespace todochallengeapi.Entities
{
    public class ToDoListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set;}
    }

}