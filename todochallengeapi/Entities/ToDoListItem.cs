namespace todochallengeapi.Entities
{
    public class ToDoListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set;}
    }

    public enum TaskStatus
    {
        completed,
        notcompleted
    }
}