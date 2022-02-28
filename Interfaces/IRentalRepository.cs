using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        Rental GetRentalById(int id);
        void AddRental(Rental rental);
        void UpdateRental(Rental rental);
        void DeleteRental(int id);

        void Save();
    }
}
