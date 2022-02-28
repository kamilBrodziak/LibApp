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

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IRentalRepository _rentalRepository;
        public NewRentalsController(ICustomerRepository customerRepository, IBookRepository bookRepository, IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }


        [HttpPost]
        public IActionResult CreateNewRental([FromBody] NewRentalDto newRental)
        {
            var customer = _customerRepository.GetCustomerById(newRental.CustomerId);

            var books = _bookRepository.GetBooks()
                .Where(b => newRental.BookIds.Contains(b.Id)).ToList();

            foreach (var book in books)
            {
                if (book.NumberAvailable == 0)
                    return BadRequest("Book is not available");

                book.NumberAvailable--;
                var rental = new Rental()
                {
                    Customer = customer,
                    Book = book,
                    DateRented = DateTime.Now
                };
                _rentalRepository.AddRental(rental);
            }
            _rentalRepository.Save();
            return Ok();
        }

    }
}
