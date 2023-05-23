using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuthors.Entities;

namespace WebApiAuthors.Controllers
{
    [ApiController]
    [Route("/api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AuthorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Authors>>> GetAuthors()
        {
            List<Authors> authors = await context.Authors.Include(x => x.Books).ToListAsync();
            
            return Ok(authors);
        }

        [HttpPost]
        public async Task<ActionResult> PostAuthors(Authors authors)
        {
            context.Add(authors);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAuthors(Authors authors, int id)
        {
            if ( authors.Id != id )
            {
                return NotFound();
            }

            context.Authors.Update(authors);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAuthors(int id)
        {
            Authors authors = await context.Authors.FindAsync(id);

            if(authors == null )
            {
                return NotFound();
            }

            context.Authors.Remove(authors);

            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
