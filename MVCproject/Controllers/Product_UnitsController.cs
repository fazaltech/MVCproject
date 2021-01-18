using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class Product_UnitsController : Controller
    {

        private mvc_pos_conn db = new mvc_pos_conn();
        // GET: Product_Unit
        public ActionResult Index(string productsunt, string ptu_name)
        {
            var Lst = new List<string>();

            var Qry = from d in db.tblproductunits
                      orderby d.unit_name
                      select d.unit_name;
            Lst.AddRange(Qry.Distinct());
            ViewBag.productsunts = new SelectList(Lst);

            var prctv = from m in db.tblproductunits
                        where m.flag == "1"
                        select m;
            if (!String.IsNullOrEmpty(productsunt))
            {
                prctv = prctv.Where(s => s.unit_name.Contains(productsunt));
            }
            if (!string.IsNullOrEmpty(ptu_name))
            {
                prctv = prctv.Where(x => x.unit_name == ptu_name);
            }




            return View(prctv);
        }

        // GET: Product_Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductunit tblproductUnity = db.tblproductunits.Find(id);
            if (tblproductUnity == null)
            {
                return HttpNotFound();
            }
            return View(tblproductUnity);
        }

        // GET: Product_Units/Create
        [HttpGet]
        public ActionResult Add_Units()
        {
            return View();
        }

        [HttpPost, ActionName("Add_Units")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Units(tblproductunit add_unit, string prounit)
        {

            add_unit.unit_name = prounit;


            Thread.Sleep(200);
            var precheck = db.tblproductunits.Where(x => x.unit_name == add_unit.unit_name).FirstOrDefault();
            var rdnum = new System.Random();
            int random = rdnum.Next(100);

            string dd = DateTime.Now.ToString("yyMMddhhmmss");
            string catid = "puid" + dd + random;


            if (precheck != null)
            {
                ViewBag.chk = "Unit Already Exist";
                return View(add_unit);

            }
            else if (ModelState.IsValid)
            {
                add_unit.unit_id = catid;
                add_unit.unit_name = prounit;
                add_unit.flag = "1";

                db.tblproductunits.Add(add_unit);
                db.SaveChanges();


            }
            ViewBag.Message = "Product Unit Added";
            return View();


        }

        // GET: Product_Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductunit tblproductUnit = db.tblproductunits.Find(id);
            var name = db.tblproductunits
                  .Where(x => x.id == id)
                  .Select(x => x.unit_name).Max();
            ViewBag.preprountname = name;
            if (tblproductUnit == null)
            {
                return HttpNotFound();
            }
            return View(tblproductUnit);
        }

        // POST: Product_Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Unity_id,Unity_name,flag")] tblproductunit tblproductUnit, string prounted, int? id)
        {

            if (ModelState.IsValid)
            {
                var prodtcat = db.tblproductunits.SingleOrDefault(b => b.id == id);
                prodtcat.unit_name = prounted;

                db.SaveChanges();
                ViewBag.MessageED = "Product Units Update";

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (tblproductUnit == null)
            {
                return HttpNotFound();
            }

            return View(tblproductUnit);
        }

        // GET: Product_Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductunit tblproductUnit = db.tblproductunits.Find(id);
            var name = db.tblproductunits
                 .Where(x => x.id == id)
                 .Select(x => x.unit_name).Max();
            ViewBag.preprocatnamedt = name;
            if (tblproductUnit == null)
            {
                return HttpNotFound();
            }
            return View(tblproductUnit);
        }

        // POST: Product_Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm([Bind(Include = "id,Unity_id,Unity_name,flag")] tblproductunit tblproductUnit, int? id)
        {

            if (ModelState.IsValid)
            {
                var prodtcatdt = db.tblproductunits.SingleOrDefault(b => b.id == id);
                prodtcatdt.flag = "0";

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (tblproductUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.MessageDT = "Product Unit Delete";
            return View(tblproductUnit);
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