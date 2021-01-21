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
    public class Product_DetailsController : Controller
    {
        private mvc_pos_conn db = new mvc_pos_conn();
        // GET: Product_Details
        public ActionResult Index()
        {


            //var Lst = new List<string>();

            //var Qry = from d in db.tblproductcategorys
            //          orderby d.category_name
            //          select d.category_name;
            //Lst.AddRange(Qry.Distinct());
            //ViewBag.productscats = new SelectList(Lst);

            //var prctv = from m in db.tblproductcategorys
            //            where m.flag == "1"
            //            select m;
            //if (!String.IsNullOrEmpty(productscat))
            //{
            //    prctv = prctv.Where(s => s.category_name.Contains(productscat));
            //}
            //if (!string.IsNullOrEmpty(ptc_name))
            //{
            //    prctv = prctv.Where(x => x.category_name == ptc_name);
            //}




            //return View(prctv);
            return View();
        }


        // GET: Product_Details/Create
        [HttpGet]
        public ActionResult Add_Product_Detail()
        {
            return View();
        }

        [HttpPost, ActionName("Add_Product_Detail")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Product_Detail(tblproduct category, string procat)
        {

            //category.category_name = procat;


            //Thread.Sleep(200);
            //var precheck = db.tblproductcategorys.Where(x => x.category_name == category.category_name).FirstOrDefault();
            //var rdnum = new System.Random();
            //int random = rdnum.Next(100);

            //string dd = DateTime.Now.ToString("yyMMddhhmmss");
            //string catid = "pcid" + dd + random;


            //if (precheck != null)
            //{
            //    ViewBag.chk = "Category Already Exist";
            //    return View(category);

            //}
            //else if (ModelState.IsValid)
            //{
            //    category.category_id = catid;
            //    category.category_name = procat;
            //    category.flag = "1";

            //    db.tblproductcategorys.Add(category);
            //    db.SaveChanges();


            //}
            //ViewBag.Message = "Product Category Added";
            return View();


        }


        // GET: Product_Details/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblproductcategory tblproductcategory = db.tblproductcategorys.Find(id);
        //    var name = db.tblproductcategorys
        //          .Where(x => x.id == id)
        //          .Select(x => x.category_name).Max();
        //    ViewBag.preprocatname = name;
        //    if (tblproductcategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblproductcategory);
        //}

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
    }
}