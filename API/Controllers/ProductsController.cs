using API.RequestHelpers;
using CORE.Entities;
using CORE.Interfaces;
using CORE.Specifications;
using INFRASTRUCTURE.Data;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> reposi) : BaseApiController
    {
         
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
           
            return await CreatePageResult(reposi, spec, specParams.PageIndex, specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await reposi.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound("Requested prodcut not found");
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            reposi.Add(product);

            if(await reposi.SaveAllAsync())
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

            reposi.Update(product);

            if (await reposi.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating");
        }

        private bool ProductExists(int id)
        {
            return reposi.Exists(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await reposi.GetByIdAsync(id);

            if(product == null)
               return NotFound();

            reposi.Remove(product);

            if (await reposi.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem Deleting");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await reposi.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await reposi.ListAsync(spec));
        }

    }
}
