using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookControlller : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookControlller(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context);
            query.ID = id;
            var result = query.Handle();
            return Ok(result);
        }
        /* [HttpGet]
        public Book GetBook([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        } */

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.ID = id;
            command.Model = updateBook;
            command.Handle();
            return Ok();
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }


}