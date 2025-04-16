using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Obada_Shop.API.Data;
using Obada_Shop.API.DTOs.Requests;
using Obada_Shop.API.DTOs.Response;
using Obada_Shop.API.Model;
using Obada_Shop.API.ServicesLayer;

namespace Obada_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService productService = productService;

        [HttpGet("")]
        public IActionResult GetAll([FromQuery] string? query, [FromQuery] int page, [FromQuery] int limit = 10)
        {
            IQueryable<Product>products = (IQueryable<Product>)productService.GetAll();
            if (query != null)
            {
                products = products.Where(Product => Product.Name.Contains(query) || Product.Description.Contains(query));
            }
            if (page <= 0 || limit <= 0)
            {
                page = 1;
                limit = 10;
            }
            products = products.Skip( (page-1) * limit).Take(limit);

            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var product = productService.Get(e => e.Id == id);
            return product == null ? NotFound() : Ok(product.Adapt<ProductResponse>());
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] ProductRequest productRequest)
        {
            var file = productRequest.mainImg;
            var product = productRequest.Adapt<Product>();
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.mainImg = fileName;
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] ProductUpdateRequest productRequest)
        {
            var productInDb = productService.Edit(id);
            var product = productRequest.Adapt<Product>();
            var file = productRequest.mainImg;

            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", product.mainImg);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                product.mainImg = fileName;
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, bool product)
        {
            var productInDb = productService.Remove(id);            
            
            return NoContent();
        }

    }
}
