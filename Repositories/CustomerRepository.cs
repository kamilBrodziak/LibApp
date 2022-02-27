using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer) => _context.Customers.Add(customer);

        public void DeleteCustomer(int id) => _context.Customers.Remove(GetCustomerById(id));

        public Customer GetCustomerById(int id) => _context.Customers.Include(c => c.MembershipType).SingleOrDefault(b => b.Id == id);

        public IEnumerable<Customer> GetCustomers() => _context.Customers.Include(c => c.MembershipType).ToList();
        public void Save() => _context.SaveChanges();

        public void UpdateCustomer(Customer customer) => _context.Customers.Update(customer);
    }
}
