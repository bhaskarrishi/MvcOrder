using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcOrder.Data;
using System.Data;
using MvcOrder.Models;

namespace MvcOrder.Controllers
{
    public class OrdersController : Controller
    {
        
        private readonly MvcOrderContext _context;
        public float Subtotal=0;
        public OrdersController(MvcOrderContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {   //Select sum(Totalqty) from dbo.Order
            //        int Subtotal = Order.Price(x => x.GetPropertyValue<int>("Price"));
            //        ViewBag.Subtotal = Subtotal;

            //using (var db = new _context())

            //var Subtotal = _context.Order.FromSql("SELECT SUM(Totalqty) FROM dbo.Order");
            //   .FirstOrDefaultAsync(m => m.Id == id);
            //var TotalP = _context.Order
            //    .FromSqlRaw("SELECT (Quantity * Price ) FROM dbo.Order")
            //    .ToList();
            //var TotalP = _context.Order;
            //TotalP = _context.Order.FirstOrDefaultAsync(b => b.Name == "Screws");
            //ViewBag.TotalP = TotalP;

            ViewBag.Subtotal = Subtotal;
            return View(await _context.Order.ToListAsync());

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Price,Totalqty")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Totalqty = (order.Quantity * order.Price);
                //Subtotal = (Subtotal + order.Totalqty);
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,Price,Totalqty")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.Totalqty = (order.Quantity * order.Price);
                    Subtotal = (Subtotal + order.Totalqty) ;
                    ViewBag.Subtotal = Subtotal;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
