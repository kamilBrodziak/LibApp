using Microsoft.AspNetCore.Mvc;
using System;
using LibApp.Models;
using LibApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using LibApp.Interfaces;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;

        public BooksController(IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetBooks();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return Content("Book not found");
            }

            return View(book);
        }
        [Authorize(Roles = "StoreManager,Owner")]
        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _genreRepository.GetGenres()
            };

            return View("BookForm", viewModel);
        }
        [Authorize(Roles = "StoreManager,Owner")]
        public IActionResult New()
        {
            var genres = _genreRepository.GetGenres();
            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View("BookForm", viewModel);
        }
        [Authorize(Roles = "StoreManager,Owner")]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _bookRepository.AddBook(book);
            }
            else
            {
                var bookInDb = _bookRepository.GetBookById(book.Id);
                bookInDb.Name = book.Name;
                bookInDb.AuthorName = book.AuthorName;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.GenreId = book.GenreId;
                bookInDb.NumberInStock = book.NumberInStock;
            }

            try
            {
                _bookRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }


    }
}
