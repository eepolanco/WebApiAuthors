namespace WebApiAuthors.Entities
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int AuthorsId { get; set; }

        public Authors Authors { get; set; } 
    }
}
