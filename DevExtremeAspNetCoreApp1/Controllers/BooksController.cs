using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevExtremeAspNetCoreApp1.Data;
using DevExtremeAspNetCoreApp1.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace DevExtremeAspNetCoreApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DevExtremeAspNetCoreApp1Context _context;

        public BooksController(DevExtremeAspNetCoreApp1Context context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public object GetBook(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_context.Books, loadOptions);
        }

        

        // GET: api/Books/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult PutBook(int Id, string values)
        {
            var book = _context.Books.FindAsync(Id);
            if (book == null) {
                return NotFound();
            }
            
            //JsonConvert.PopulateObject(value: values, target: book);

            if (!TryValidateModel(book))
                return BadRequest();

            _context.SaveChanges();

            return Ok(book);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostBook(string values)
        {
            //var book = new Book();
            //JsonConvert.PopulateObject(values, book);
            Book book = JsonSerializer.Deserialize<Book>(values);

            if (!TryValidateModel(book))
                return BadRequest();
            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Books/5
        [HttpDelete]
        public void DeleteBook(int key)
        {
            var order = _context.Books.Find(key);
            _context.Books.Remove(order);
            _context.SaveChanges();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
