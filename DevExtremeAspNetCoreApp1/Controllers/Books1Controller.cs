using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevExtremeAspNetCoreApp1.Data;
using DevExtremeAspNetCoreApp1.Models;

namespace DevExtremeAspNetCoreApp1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Books1Controller : Controller
    {
        private DevExtremeAspNetCoreApp1Context _context;

        public Books1Controller(DevExtremeAspNetCoreApp1Context context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var books = _context.Books.Select(i => new {
                i.Id,
                i.Title,
                i.Genre,
                i.PublishDate,
                i.Price
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(books, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Book();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Books.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Books.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Books.FirstOrDefaultAsync(item => item.Id == key);

            _context.Books.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Book model, IDictionary values) {
            string ID = nameof(Book.Id);
            string TITLE = nameof(Book.Title);
            string GENRE = nameof(Book.Genre);
            string PUBLISH_DATE = nameof(Book.PublishDate);
            string PRICE = nameof(Book.Price);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(TITLE)) {
                model.Title = Convert.ToString(values[TITLE]);
            }

            if(values.Contains(GENRE)) {
                model.Genre = Convert.ToString(values[GENRE]);
            }

            if(values.Contains(PUBLISH_DATE)) {
                model.PublishDate = Convert.ToDateTime(values[PUBLISH_DATE]);
            }

            if(values.Contains(PRICE)) {
                model.Price = Convert.ToInt64(values[PRICE]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}