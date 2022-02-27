using LibApp.Models;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        Genre GetGenreById(int id);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int id);

        void Save();
    }
}
