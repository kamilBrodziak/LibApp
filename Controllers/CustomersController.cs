using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Interfaces;

namespace LibApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public CustomersController(ICustomerRepository customerRepository, IMembershipTypeRepository membershipTypeRepository)
        {
            _customerRepository = customerRepository;
            _membershipTypeRepository = membershipTypeRepository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        public IActionResult New()
        {
            var membershipTypes = _membershipTypeRepository.GetMembershipTypes();

            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };


            return View("CustomerForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _membershipTypeRepository.GetMembershipTypes()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _membershipTypeRepository.GetMembershipTypes(),
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0) 
            { 
                _customerRepository.AddCustomer(customer);
            }
            else
            {
                var customerInDb = _customerRepository.GetCustomerById(customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.HasNewsletterSubscribed = customer.HasNewsletterSubscribed;
            }

            try
            {
                _customerRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Customers");
        }
    }
}