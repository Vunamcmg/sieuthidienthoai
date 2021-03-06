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
    public class SupplierController : Controller
    {
        private readonly SieuThiDienThoaiDbContext _context;

        public SupplierController(SieuThiDienThoaiDbContext context)
        {
            _context = context;
        }
        // GET: Supplier/Details/id
        [HttpGet]
        public async Task<IActionResult> SupplierDetails(int id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            return Ok(supplier);
        }

        //POST: Supplier/Create
        [HttpPost]
        public IActionResult SupplierCreate([FromBody] Supplier supplier)
        {
            Supplier test = new Supplier();
            test = supplier;
            if (ModelState.IsValid)
            {
                _context.Add(test);
                _context.SaveChangesAsync();
            }
            return Json("Success");
        }

        // DELETE: Supplier/Delete/id
        [HttpPost]
        public async Task<IActionResult> SupplierDelete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return Json("Success");
        }

        // UPDATE: Supplier/Edit/id
        [HttpPost]
        public async Task<IActionResult> SupplierEdit(int id,[FromBody] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return Json("Fail");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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

        // GETALL: Supplier/Index
        [HttpGet]
        public async Task<IActionResult> SupplierIndex()
        {
            return Json(await _context.Suppliers.ToListAsync());
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
