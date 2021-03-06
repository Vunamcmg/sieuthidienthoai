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
    public class BillController : Controller
    {
        private readonly SieuThiDienThoaiDbContext _context;

        public BillController(SieuThiDienThoaiDbContext context)
        {
            _context = context;
        }

        // GET: Bill/Details/id
        [HttpGet]
        public async Task<IActionResult> BillDetails(int id)
        {
            var bill = await _context.Bills
                .FirstOrDefaultAsync(m => m.Id == id);

            return Ok(bill);
        }

        //POST: Bill/Create
        [HttpPost]
        public IActionResult BillCreate([FromBody] Bill bill)
        {
            Bill test = new Bill();
            test = bill;
            if (ModelState.IsValid)
            {
                _context.Add(test);
                _context.SaveChangesAsync();
            }
            return Json("Success");
        }

        // DELETE: Bill/Delete/id
        [HttpPost]
        public async Task<IActionResult> BillDelete(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return Json("Success");
        }

        // UPDATE: Bill/Edit/id
        [HttpPost]
        public async Task<IActionResult> BillEdit(int id,[FromBody] Bill bill)
        {
            if (id != bill.Id)
            {
                return Json("Fail");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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

        // GETALL: Bill/Index
        [HttpGet]
        public async Task<IActionResult> BillIndex()
        {
            return Json(await _context.Bills.ToListAsync());
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
