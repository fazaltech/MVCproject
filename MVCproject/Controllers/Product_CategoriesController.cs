using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class Product_CategoriesController : Controller
    {
        private mvc_pos_conn db = new mvc_pos_conn();

        // GET: Product_Categories
        public ActionResult Index()
        {
            return View(db.tblproductcategorys.ToList());
        }

        // GET: Product_Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory tblproductcategory = db.tblproductcategorys.Find(id);
            if (tblproductcategory == null)
            {
                return HttpNotFound();
            }
            return View(tblproductcategory);
        }

        // GET: Product_Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category_id,category_name,flag")] tblproductcategory tblproductcategory)
        {
            if (ModelState.IsValid)
            {
                db.tblproductcategorys.Add(tblproductcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblproductcategory);
        }

        // GET: Product_Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory tblproductcategory = db.tblproductcategorys.Find(id);
            if (tblproductcategory == null)
            {
                return HttpNotFound();
            }
            return View(tblproductcategory);
        }

        // POST: Product_Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,category_id,category_name,flag")] tblproductcategory tblproductcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblproductcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblproductcategory);
        }

        // GET: Product_Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory tblproductcategory = db.tblproductcategorys.Find(id);
            if (tblproductcategory == null)
            {
                return HttpNotFound();
            }
            return View(tblproductcategory);
        }

        // POST: Product_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblproductcategory tblproductcategory = db.tblproductcategorys.Find(id);
            db.tblproductcategorys.Remove(tblproductcategory);
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
