using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;
        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddRental(Rental rental) => _context.Rentals.Add(rental);

        public void DeleteRental(int id) => _context.Rentals.Remove(GetRentalById(id));

        public Rental GetRentalById(int id) => _context.Rentals.SingleOrDefault(b => b.Id == id);

        public IEnumerable<Rental> GetRentals() => _context.Rentals.ToList();

        public void Save() => _context.SaveChanges();

        public void UpdateRental(Rental rental) => _context.Rentals.Update(rental);
    }
}
