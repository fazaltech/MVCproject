﻿using System;
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
    public class Product_DetailsController : Controller
    {
        private mvc_pos_conn db = new mvc_pos_conn();
        // GET: Product_Details
        [HttpGet]
        public ActionResult Index()
        {


            var pro =( from d in db.tblproducts
                      where d.flag=="1"
                      join u in db.tblproductunits on d.unit_id equals u.unit_id
                      join c in db.tblproductcategorys on d.category_id equals c.category_id
                      select new {
                          d.product_name,
                          u.unit_name,
                          c.category_name,
                          d.unit_in_stock,
                          d.unit_price,
                          d.recorder_level
                          
                      }).ToList();

            //return Json(new { data = pro }, JsonRequestBehavior.AllowGet);
            return View();
        }


        // GET: Product_Details/Create
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Add_Product_Detail()
        {
            
            return View();
        }

        [HttpPost, ActionName("Add_Product_Detail")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Product_Detail(tblproduct product,productview product_views)
        {

            
            product.product_name = product_views.name;
           string name= product_views.name;
            string proc = name.Substring(0,4);


            Thread.Sleep(200);
            var precheck = db.tblproducts.Where(x => x.product_name == product.product_name).Where(x=>x.flag=="1").FirstOrDefault();
            var rdnum = new System.Random();
            int random = rdnum.Next(100);


            var unitids = db.tblproductunits.Where(x => x.unit_name == product_views.unit_name).Select(x => x.unit_id).Max();
            string unitid = unitids;
            var catproids = db.tblproductcategorys.Where(x => x.category_name == product_views.cat_name).Select(x => x.category_id).Max();
            string catid = catproids;



            string dd = DateTime.Now.ToString("yyMMddhhmmss");
            string proid = "ppid" + dd + random;
            string procode = proc+product_views.price;

            if (precheck != null)
            {

                return Json(new { success = false, responseText = "Product Already Exist" }, JsonRequestBehavior.AllowGet);
               

            }
            else if (ModelState.IsValid)
            {
                product.product_id = proid;
                product.product_code = procode;
                product.unit_id = unitid;
                product.category_id = catid;
                product.product_name = product_views.name;
                product.flag = "1";

                db.tblproducts.Add(product);
                db.SaveChanges();


            }

            return Json(new { success = true, responseText = "Product Added" }, JsonRequestBehavior.AllowGet);


        }


        //Get: Product_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproducts = db.tblproducts.Find(id);
            var name = db.tblproducts
                  .Where(x => x.id == id)
                  .Select(x => x.product_name).Max();
            ViewBag.preprocatname = name;
            if (tblproducts == null)
            {
                return HttpNotFound();
            }
            return View(tblproducts);
        }

        //// POST: Product_Details/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,category_id,category_name,flag")] tblproductcategory tblproductcategory, string procated, int? id)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var prodtcat = db.tblproductcategorys.SingleOrDefault(b => b.id == id);
        //        prodtcat.category_name = procated;

        //        db.SaveChanges();
        //        ViewBag.MessageED = "Product Category Update";

        //    }
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (tblproductcategory == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(tblproductcategory);
        //}

        //// GET: Product_Details/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblproductcategory tblproductcategory = db.tblproductcategorys.Find(id);
        //    var name = db.tblproductcategorys
        //         .Where(x => x.id == id)
        //         .Select(x => x.category_name).Max();
        //    ViewBag.preprocatnamedt = name;
        //    if (tblproductcategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblproductcategory);
        //}

        //// POST: Product_Details/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirm([Bind(Include = "id,category_id,category_name,flag")] tblproductcategory tblproductcategory, int? id)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var prodtcatdt = db.tblproductcategorys.SingleOrDefault(b => b.id == id);
        //        prodtcatdt.flag = "0";

        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (tblproductcategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MessageDT = "Product Category Delete";
        //    return View(tblproductcategory);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        [HttpGet]
        public JsonResult Pro_cat_view()
        {
            try
            {
                return Json(db.tblproductcategorys.Where(x => x.flag == "1").Select(x => new
                {
                    CategoryID = x.id,
                    CategoryName = x.category_name
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }


        [HttpGet]
        public JsonResult Pro_unit_view()
        {


            try
            {
                return Json(db.tblproductunits.Where(x => x.flag == "1").Select(x => new
                {
                    UnitID = x.id,
                    UnitName = x.unit_name
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }


        public JsonResult TableView()
        {
            List<Producttable> tt = new List<Producttable>();

            try
            {
                var pro = (from d in db.tblproducts
                           where d.flag == "1"
                           join u in db.tblproductunits on d.unit_id equals u.unit_id
                           join c in db.tblproductcategorys on d.category_id equals c.category_id
                           select new
                           {
                               d.id,
                               d.product_name,
                               u.unit_name,
                               c.category_name,
                               d.unit_in_stock,
                               d.unit_price,
                               d.recorder_level

                           }).ToList();


              


                return Json(new { data = pro }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }

        public class productview
        {
            public string name { get; set; }
            public string unit_name { get; set; }
            public string cat_name { get; set; }
            public string price { get; set; }
            public string dist_pre { get; set; }


        }


        public class Producttable 
        {
            public int id { get; set; }
            public string product_name { get; set; }
            public string unit_name { get; set; }
            public string category_name { get; set; }
            public decimal unit_in_stock { get; set; }
            public decimal unit_price { get; set; }
            public decimal recorder_level{ get; set; }

        }
    }
}