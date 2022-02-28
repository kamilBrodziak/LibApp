using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        [Authorize(Roles = "StoreManager,Owner")]
        public IActionResult GetCustomers(string query = null)
        {
            IEnumerable<Customer> customersQuery = _customerRepository.GetCustomers();

            if (!string.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }

            var customerDtos = customersQuery
                .ToList()
                .Select(_mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // GET /api/customers/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "StoreManager,Owner")]
        public IActionResult GetCustomer(int id)
        {
            Console.WriteLine("Request beginning");

            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            Console.WriteLine("Request end");

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        // POST /api/customers/
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var customer = _mapper.Map<Customer>(customerDto);
            _customerRepository.AddCustomer(customer);
            _customerRepository.Save();
            customerDto.Id = customer.Id;

            return CreatedAtRoute(nameof(GetCustomer), new { id = customerDto.Id }, customerDto);
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public IActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var customerInDb = _customerRepository.GetCustomerById(id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }


            _mapper.Map(customerDto, customerInDb);
            _customerRepository.UpdateCustomer(customerInDb);
            _customerRepository.Save();
            return Ok(customerInDb);
        }

        // DELETE /api/customers
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerInDb = _customerRepository.GetCustomerById(id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            _customerRepository.DeleteCustomer(id);
            _customerRepository.Save();
            return NoContent();
        }

    }
}
