namespace GoRestAPITestFramework.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string DueOn { get; set; }
        public string Status { get; set; }
    }
}