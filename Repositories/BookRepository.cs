using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBook(Book book) => _context.Books.Add(book);

        public void DeleteBook(int id) => _context.Books.Remove(GetBookById(id));

        public Book GetBookById(int id) => _context.Books.Include(b => b.Genre).SingleOrDefault(b => b.Id == id);

        public IEnumerable<Book> GetBooks() => _context.Books.Include(b => b.Genre).ToList();

        public void Save() => _context.SaveChanges();

        public void UpdateBook(Book book) => _context.Books.Update(book);
    }
}
