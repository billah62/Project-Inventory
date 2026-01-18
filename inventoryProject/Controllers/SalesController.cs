using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using inventoryProject.Models;

namespace inventoryProject.Controllers
{
    public class SalesController : Controller
    {
        private StockManageEntities db = new StockManageEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.customer).Include(s => s.store).Include(s => s.product);
            return View(sales.ToList());
        }
        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }
        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name");
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name");
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status");
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sale_id,product_id,customer_id,store_id,sale_date,rate,quantity,total_price,vat,discount,net_total_price,stock_status,memo_no,coomments")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);

                // update stock
                var oStock = (from o in db.stocks where o.product_id == sale.product_id select o).FirstOrDefault();
                if (oStock != null)
                {
                    oStock.quantity -= sale.quantity;
                    oStock.status = "out";
                }
                // end of update stock

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", sale.customer_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", sale.store_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", sale.product_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", sale.product_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status", sale.sale_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status", sale.sale_id);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", sale.customer_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", sale.store_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", sale.product_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", sale.product_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status", sale.sale_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status", sale.sale_id);
            return View(sale);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sale_id,product_id,customer_id,store_id,sale_date,rate,quantity,total_price,vat,discount,net_total_price,stock_status,memo_no,coomments")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "customer_name", sale.customer_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", sale.store_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", sale.product_id);
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", sale.product_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status", sale.sale_id);
            ViewBag.sale_id = new SelectList(db.Sales, "sale_id", "stock_status", sale.sale_id);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);

            // update stock
            var oStock = (from o in db.stocks where o.product_id == sale.product_id select o).FirstOrDefault();
            if (oStock != null)
            {
                oStock.quantity += sale.quantity;
                oStock.status = "in";
            }
            // end of update stock

            db.Sales.Remove(sale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
