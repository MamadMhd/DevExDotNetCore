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
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

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
            return DataSourceLoader.Load(_context.Book, loadOptions);
        }

        

        // GET: api/Books/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public IActionResult PutBook(int key, string values)
        {
            var book = BookExists(key);
            JsonConvert.PopulateObject(values, book);

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
            var newBook = new Book();
            JsonConvert.PopulateObject(values, newBook);

            if (!TryValidateModel(newBook))
                return BadRequest();

            _context.Book.Add(newBook);
            _context.SaveChanges();

            return Ok(newBook);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public void DeleteBook(int key)
        {
            var order = _context.Book.First(e => e.Id == key);
            _context.Book.Remove(order);
            _context.SaveChanges();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
