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
    public class BrandsController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService brandService = brandService;


        [HttpGet("")]
        public IActionResult getAll()
        {
            var brands = brandService.GetAll();
            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {
            var brand = brandService.Get(e => e.Id == id);
            return brand == null ? NotFound() : Ok(brand.Adapt<BrandResponse>());

        }

        [HttpPost("")]

        public IActionResult Create([FromBody] BrandRequest brandRequest)
        {
            var brandInDb = brandService.Add(brandRequest.Adapt<Brand>());
            return CreatedAtAction(nameof(getById), new { brandInDb.Id }, brandInDb);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest brandRequest)
        {
            var brandInDb = brandService.Edit(id, brandRequest.Adapt<Brand>());
            if (!brandInDb) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var brandInDb = brandService.Remove(id);
            if (!brandInDb) return NotFound();
            return NoContent();
        }
    }
}
