using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obada_Shop.API.Data;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult getAll()
        {
            var categories = _context.categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {
            var category = _context.categories.Find(id);
            return category == null ? NotFound() : Ok(category);

        }

        [HttpPost("")]

        public IActionResult Create([FromBody] Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new {category.Id},category);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var category = _context.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
