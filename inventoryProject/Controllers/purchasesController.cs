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
    public class purchasesController : Controller
    {
        private StockManageEntities db = new StockManageEntities();
        // GET: purchases
        public ActionResult Index()
        {
            var purchases = db.purchases.Include(p => p.product).Include(p => p.store).Include(p => p.supplier);
            return View(purchases.ToList());
        }

        // GET: purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase purchase = db.purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: purchases/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name");
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchase_id,product_id,supplier_id,store_id,purchase_date,unit_price,quantity,total_price,vat,grand_total_price,stock_status,memo_no,coomments")] purchase purchase)
        {
            if (ModelState.IsValid)
            {

                purchase opurchase = new purchase();
                // insert into purchase
                //opurchase.unit_price = purchase.unit_price;
                //opurchase.grand_total_price = purchase.unit_price * purchase.quantity+purchase.vat;
                db.purchases.Add(purchase);

                // update stock
                var oStock = (from o in db.stocks where o.product_id == purchase.product_id select o).FirstOrDefault();
                if (oStock == null)
                {
                    oStock = new stock();
                    oStock.product_id = purchase.product_id;
                    oStock.quantity = purchase.quantity;
                    oStock.store_id = purchase.store_id;
                    oStock.status = "in";
                    db.stocks.Add(oStock);
                }
                else
                {
                    oStock.quantity += purchase.quantity;
                    oStock.status = "in";
                }
                // end of update stock

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", purchase.product_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", purchase.store_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", purchase.supplier_id);
            return View(purchase);

        }

        // GET: purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase purchase = db.purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", purchase.product_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", purchase.store_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", purchase.supplier_id);
            return View(purchase);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "purchase_id,product_id,supplier_id,store_id,purchase_date,unit_price,quantity,total_price,vat,grand_total_price,stock_status,memo_no,coomments")] purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", purchase.product_id);
            ViewBag.store_id = new SelectList(db.stores, "store_id", "store_name", purchase.store_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", purchase.supplier_id);
            return View(purchase);
        }

        // GET: purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase purchase = db.purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            purchase purchase = db.purchases.Find(id);
            // update stock
            var oStock = (from o in db.stocks where o.product_id == purchase.product_id select o).FirstOrDefault();
            if (oStock != null)
            {
                oStock.quantity -= purchase.quantity;
                oStock.status = "out";
            }
            // end of update stock
            db.purchases.Remove(purchase);
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
