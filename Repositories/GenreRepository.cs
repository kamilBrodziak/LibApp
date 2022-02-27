using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddGenre(Genre genre) => _context.Genre.Add(genre);

        public void DeleteGenre(int id) => _context.Genre.Remove(GetGenreById(id));

        public Genre GetGenreById(int id) => _context.Genre.Find(id);

        public IEnumerable<Genre> GetGenres() => _context.Genre.ToList();

        public void Save() => _context.SaveChanges();

        public void UpdateGenre(Genre genre) => _context.Genre.Update(genre);
    }
}
