using CORE.Entities;
using CORE.Interfaces;
using INFRASTRUCTURE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository reposi) : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, 
            string? type, string? sort)
        {
            return Ok(await reposi.GetProductAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await reposi.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound("Requested prodcut not found");
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            reposi.AddProduct(product);

            if(await reposi.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if(product.Id != id || !ProductExists(id))
            {
                return NotFound("Requested prodcut not found");
            }

            reposi.UpdateProduct(product);

            if (await reposi.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating");
        }

        private bool ProductExists(int id)
        {
            return reposi.ProductExists(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await reposi.GetProductByIdAsync(id);

            if(product == null)
               return NotFound();

            reposi.DeleteProduct(product);

            if (await reposi.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem Deleting");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await reposi.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await reposi.GetTypesAsync());
        }

    }
}
