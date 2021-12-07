using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderListController : Controller
    {

            private ApplicationDbContext _db;

            public OrderListController(ApplicationDbContext db)
            {
                _db = db;
            }
            [AllowAnonymous]
            public IActionResult Index()
            { 
                return View(_db.Orders.ToList());
            }


        //GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Update(order);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //GET Delete Action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Order order)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != order.Id)
            {
                return NotFound();
            }

            var Id = _db.Orders.Find(id);
            if (Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(Id);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        public ActionResult Order_ProductList(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            return View(_db.OrderDetails.Where(c => c.OrderId == id).ToList());
        }
    }
}
