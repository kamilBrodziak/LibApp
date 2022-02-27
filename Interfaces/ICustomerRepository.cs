using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);

        void Save();
    }
}
