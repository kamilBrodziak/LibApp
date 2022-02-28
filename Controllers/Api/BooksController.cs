using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<BookDto> GetBooks(string query = null) 
        {
            var booksQuery = _bookRepository.GetBooks()
                .Where(b => b.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
            {
                booksQuery = booksQuery.Where(b => b.Name.Contains(query));
            }

            return booksQuery.ToList().Select(_mapper.Map<Book, BookDto>);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public IActionResult CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var book = _mapper.Map<Book>(bookDto);
            _bookRepository.AddBook(book);
            _bookRepository.Save();

            bookDto.Id = book.Id;

            return CreatedAtRoute(nameof(GetBook), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var bookInDb = _bookRepository.GetBookById(id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            _mapper.Map(bookDto, bookInDb);

            _bookRepository.UpdateBook(bookInDb);
            _bookRepository.Save();

            return Ok(bookInDb);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookInDb = _bookRepository.GetBookById(id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            _bookRepository.DeleteBook(id);
            _bookRepository.Save();

            return NoContent();
        }

    }
}
