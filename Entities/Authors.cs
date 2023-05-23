namespace WebApiAuthors.Entities
{
    public class Authors
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Books> Books { get; set; }
    }
}
