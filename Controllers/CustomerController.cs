using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SieuThiDienThoai.Data;
using SieuThiDienThoai.Models;

namespace SieuThiDienThoai.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SieuThiDienThoaiDbContext _context;

        public CustomerController(SieuThiDienThoaiDbContext context)
        {
            _context = context;
        }

        // GET: Customer/Details/id
        [HttpGet]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);

            return Ok(customer);
        }

        //POST: Customer/Create
        [HttpPost]
        public IActionResult CustomerCreate([FromBody] Customer customer)
        {
            Customer test = new Customer();
            test = customer;
            if (ModelState.IsValid)
            {
                _context.Add(test);
                _context.SaveChangesAsync();
            }
            return Json("Success");
        }

        // DELETE: Customer/Delete/id
        [HttpPost]
        public async Task<IActionResult> CustomerDelete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Json("Success");
        }

        // UPDATE: Customer/Edit/id
        [HttpPost]
        public async Task<IActionResult> CustomerEdit(int id,[FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                return Json("Fail");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return Json("Not found");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Json("Success");
        }

        // GETALL: Customer/Index
        [HttpGet]
        public async Task<IActionResult> CustomerIndex()
        {
            return Json(await _context.Customers.ToListAsync());
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
