using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.Data;
using ProductCRUD.Models;

namespace ProductCRUD.Controllers
{
    public class ProductController(AppDbContext context) : Controller
    {
        
        public IActionResult Index()
        {
            var products = context.Products.ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View();

            product.CreateAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            context.Add(product);

           var result = context.SaveChanges();
           
            if(result >0)
            {
                TempData["Message"] = "Product created sucessfully!";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = context.Products.SingleOrDefault(x=>x.Id==id);

            if (product is null) return NotFound();


            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product, int id)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var productFromDb = context.Products.SingleOrDefault(x=>x.Id==id);

            if (productFromDb is null) return NotFound();

            productFromDb.Name = product.Name;
            productFromDb.Image = product.Image;
            productFromDb.Description = product.Description;
            productFromDb.UpdatedAt = DateTime.Now;

            if (context.SaveChanges() > 0)
            {
                TempData["Message"] = "Product updated sucessfully!";

                return RedirectToAction(nameof(Index));

            }
            TempData["Message"] = "Product failed to update";

            return View();


        }


        [HttpDelete("api/products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = context.Products.SingleOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            context.Products.Remove(product);

            if(context.SaveChanges() > 0)
            {
                return Ok(new {message="Product deleted successfully!"});
            }

            return BadRequest(new { message = "Failed to update product." });

        }


    }
}
