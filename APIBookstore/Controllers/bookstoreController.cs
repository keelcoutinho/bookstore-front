using APIBookstore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookstoreController : ControllerBase
    {
        private readonly TodoContext _context;



        public bookstoreController(TodoContext context)
        {

            _context = context;

            foreach (Product x in _context.TodoProducts)
                _context.TodoProducts.Remove(x);
            _context.SaveChanges();


            _context.TodoProducts.Add(new Product { Id = "1", Name = "O Sol é Para Todos", Price = 24, Quantity = 1, Category = "romance", Img = "img1" });
            _context.TodoProducts.Add(new Product { Id = "2", Name = "Mulheres que correm com os lobos", Price = 39, Quantity = 1, Category = "não ficção", Img = "img2" });
            _context.TodoProducts.Add(new Product { Id = "3", Name = "O Averso da pele", Price = 40, Quantity = 2, Category = "romance", Img = "img3" });
            _context.TodoProducts.Add(new Product { Id = "4", Name = "A Revolução dos bichos", Price = 14, Quantity = 1, Category = "sátira", Img = "img4" });
            _context.TodoProducts.Add(new Product { Id = "5", Name = "Extraordinário", Price = 24, Quantity = 5, Category = "romance", Img = "img5" });
            _context.TodoProducts.Add(new Product { Id = "6", Name = "A cabana", Price = 18, Quantity = 5, Category = "suspense", Img = "img6" }); 
            



            _context.SaveChanges();



        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.TodoProducts.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdut), new { id = product.Id }, product);
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetTodoItems()
        {
            return await _context.TodoProducts.ToListAsync(); 



        }

        // GET: api/bookstore/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProdut(int id)
        {
            var todoItem = await _context.TodoProducts.FindAsync(id.ToString());

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

    }
}
