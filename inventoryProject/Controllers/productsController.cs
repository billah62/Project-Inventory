using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using inventoryProject.Models;
using inventoryProject.ViewModels;

namespace inventoryProject.Controllers
{
    public class productsController : Controller
    {
        private StockManageEntities db = new StockManageEntities();
        // GET: products
        public ActionResult Index()
        {
            //var products = db.products.Include(p => p.produc_type);
            var products = from p in db.products
                            join s in db.stocks on p.product_id equals s.product_id
                            select new VmProduct
                            {
                                product_id=p.product_id,
                                buying_price=p.buying_price,
                                photo=p.photo,
                                product_name=p.product_name,
                                product_type_id=p.product_type_id,
                                produc_type=p.produc_type,
                                selling_price=p.selling_price,
                                quantity=s.quantity
                            };

            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.product_type_id = new SelectList(db.produc_type, "product_type_id", "product_type_name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,product_type_id,buying_price,selling_price,photo")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_type_id = new SelectList(db.produc_type, "product_type_id", "product_type_name", product.product_type_id);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_type_id = new SelectList(db.produc_type, "product_type_id", "product_type_name", product.product_type_id);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_type_id,buying_price,selling_price,photo")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_type_id = new SelectList(db.produc_type, "product_type_id", "product_type_name", product.product_type_id);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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

        // GET: products/GetStockByProductId/5
        public JsonResult GetStockByProductId(int? id)
        {
            if (id == null)
            {
                return Json(new { message = "invalid product id" }, JsonRequestBehavior.AllowGet);
            }
            var oStock = db.stocks.Find(id);
            var oStockOfProduct = (from p in db.products
                      join s in db.stocks on p.product_id equals s.product_id
                      where p.product_id == id
                      select new
                      {
                          p.product_id,
                          p.selling_price,
                          s.quantity
                      }).FirstOrDefault();
            if (oStockOfProduct == null)
            {
                return Json(new { message = "product not found" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { pid = oStockOfProduct.product_id, qty = oStockOfProduct.quantity, salePrice = oStockOfProduct.selling_price }, JsonRequestBehavior.AllowGet);
        }
    }
}
