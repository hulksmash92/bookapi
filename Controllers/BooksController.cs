using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _booksService { get; }

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return _booksService.Get();
        }

        [HttpGet(("{id:length(24)}"), Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _booksService.Get(id);

            if (book == null) 
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _booksService.Create(book);

            return CreatedAtRoute(
                "GetBook", 
                new { id = book.Id.ToString() }, 
                book
            );
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _booksService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _booksService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _booksService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _booksService.Remove(book.Id);

            return NoContent();
        }
    }
}