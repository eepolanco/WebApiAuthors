using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuthors.Entities;

namespace WebApiAuthors.Controllers
{
    [ApiController]
    [Route("/api/libros")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Books>> Get(int id)
        {
            return await context.Books.Include(x => x.Authors).FirstOrDefaultAsync(b => b.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Books books)
        {
            var existAuthor = await context.Authors.AnyAsync(x => x.Id == books.AuthorsId);

            if (!existAuthor)
            {
                return BadRequest($"No existe el author con el ID: {books.AuthorsId}");
            }
            context.Books.Add(books);

            await context.SaveChangesAsync();

            return Ok();
        }

    }
}
