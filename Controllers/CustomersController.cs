﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerDemo.Models;
using System.Net.Http;

namespace SwaggerDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {

        List<Customer> Customers = new List<Customer>
       {
            new Customer { Id = 1, FirstName = "Baidy", LastName = "Thiongane", EMail = "xxxx@gmail.com"},
            new Customer { Id = 2, FirstName = "Thomas", LastName = "Perrin", EMail = "thomas@outlook.com"},
            new Customer { Id = 3, FirstName = "Allan", LastName = "Croft", EMail = "allan.croft@crt.com"},
            new Customer { Id = 4 , FirstName = "Sahra", LastName = "Parker", EMail = "sahra@yahoo.com"},
       };

        // GET: api/Customers
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<Customer> GetAll()
        {
            return Customers;
        }

        /// <summary>
        /// Retourne un client specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client a retourné</param>   
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult GetById(int id)
        {
            var custormer = Customers.FirstOrDefault(c => c.Id == id);
            if (custormer == null)
            { return NotFound(); }
            return new ObjectResult(custormer);
        }
        
        // POST: api/Customers
        [HttpPost]
        public IActionResult Create([FromBody]Customer customer)
        {
            if(customer==null)
            {
                return BadRequest();
            }

            Customers.Add(customer);

            return CreatedAtRoute("GetById", new { id = customer.Id }, customer);

        }
        
        // PUT: api/Customers/5
        [HttpPut("{id}")]
        [ApiExplorerSettings(GroupName = "v2")]
        public IActionResult Update(int id, [FromBody]Customer customer)
        {

            if (customer== null || customer.Id != id)
            {
                return BadRequest();
            }

            var existingCustomer = Customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }


            Customers.Remove(existingCustomer);
            Customers.Add(customer);
            return new NoContentResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(GroupName = "v2")]
        
        public IActionResult Delete(int id)
        {
            var customer = Customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

           Customers.Remove(customer);
         return new NoContentResult();
        }
    }
}
