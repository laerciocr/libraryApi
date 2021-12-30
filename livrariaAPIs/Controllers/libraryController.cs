using Microsoft.AspNetCore.Mvc;
using livrariaAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace livrariaAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class libraryController : ControllerBase
    {
        private readonly ToDoContext _context;
        public libraryController(ToDoContext context)
        {
            _context = context;
            _context.allProducts.Add(new Product
            {
                Id = "1",
                Name = "C# 9.0 in a Nutshell: The Definitive Reference",
                Price = 45.0,
                Amount = 1,
                Category = "Reference",
                Image = "Imag_1"
            });
            _context.allProducts.Add(new Product
            {
                Id = "2",
                Name = "C# in Depth",
                Price = 40.0,
                Amount = 1,
                Category = "Advanced",
                Image = "Imag_2"
            });
            _context.SaveChangesAsync();
            _context.allProducts.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.allProducts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            Product product = await _context.allProducts.FindAsync(id.ToString());

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.allProducts.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product product = await _context.allProducts.FindAsync(id.ToString());
            if (product == null)
            {
                return NotFound();
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
