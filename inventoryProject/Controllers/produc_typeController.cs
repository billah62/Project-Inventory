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
    public class produc_typeController : Controller
    {
        private StockManageEntities db = new StockManageEntities();

        // GET: produc_type
        public ActionResult Index()
        {
            return View(db.produc_type.ToList());
        }
        // GET: produc_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produc_type produc_type = db.produc_type.Find(id);
            if (produc_type == null)
            {
                return HttpNotFound();
            }
            return View(produc_type);
        }

        // GET: produc_type/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_type_id,product_type_name")] produc_type produc_type)
        {
            if (ModelState.IsValid)
            {
                db.produc_type.Add(produc_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produc_type);
        }

        // GET: produc_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produc_type produc_type = db.produc_type.Find(id);
            if (produc_type == null)
            {
                return HttpNotFound();
            }
            return View(produc_type);
        }

        // POST: produc_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_type_id,product_type_name")] produc_type produc_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produc_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produc_type);
        }

        // GET: produc_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produc_type produc_type = db.produc_type.Find(id);
            if (produc_type == null)
            {
                return HttpNotFound();
            }
            return View(produc_type);
        }

        // POST: produc_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            produc_type produc_type = db.produc_type.Find(id);
            db.produc_type.Remove(produc_type);
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
