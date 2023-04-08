namespace GoRestAPITestFramework.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}