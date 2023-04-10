namespace GoRestAPITestFramework.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string? Title { get; set; }
        public string? DueOn { get; set; }
        public string? Status { get; set; }
    }
}