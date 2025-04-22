using COMP003.LectureActivity5.Data;
using COMP003.LectureActivity5.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP003.LectureActivity5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        //retrieve products
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(ProductStore.Products);
        }

        //retreive via ID
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = ProductStore.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }    

            return Ok(product);
        }
        //create product
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = ProductStore.Products.Max(p => p.Id) + 1;

            ProductStore.Products.Add(product);

            //Return 201 
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        //update product
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {
            //check if product exist
            var existingProduct = ProductStore.Products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            //return 204 to indicate success
            return NoContent();
        }

    }
}
