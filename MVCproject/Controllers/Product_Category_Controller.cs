using MVCproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVCproject.Controllers
{
    public class Product_Category_Controller : Controller
    {
        private mvc_pos_conn db = new mvc_pos_conn();
        // GET: Product_Category_


        public ActionResult Index(string name,string abc)
        {
            var Lst = new List<string>();

            var Qry = from d in db.tblproductcategories
                      orderby d.category_name
                      select d.category_name;
            Lst.AddRange(Qry.Distinct());
            ViewBag.roles = new SelectList(Lst);

            var prctv = from m in db.tblproductcategories
                       where m.flag == "1"
                       select m;
            if (!String.IsNullOrEmpty(name))
            {
                prctv = prctv.Where(s => s.category_name.Contains(name));
            }
            if (!string.IsNullOrEmpty(abc))
            {
                prctv = prctv.Where(x => x.category_name == abc);
            }




            return View(prctv);
        }



        public ActionResult Add_Category()
        {
            return View();
        }
        [HttpPost, ActionName("Add_Category")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Category(tblproductcategory category, string procat)
        {

            category.category_name = procat;


            Thread.Sleep(200);
            var precheck = db.tblproductcategories.Where(x => x.category_name == category.category_name).FirstOrDefault();
            var rdnum = new System.Random();
            int random = rdnum.Next(100);

            string dd = DateTime.Now.ToString("yyMMddhhmmss");
            string catid = "pcid" + dd + random;


            if (precheck != null)
            {
                ViewBag.chk = "User Already Exist";
                return View(category);

            }
            else if (ModelState.IsValid)
            {
                category.category_id = catid;
                category.category_name = procat;
                category.flag = "1";

                db.tblproductcategories.Add(category);
                db.SaveChanges();


            }
      
            return View();


        }

        public ActionResult Edit_Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory prodct = db.tblproductcategories.Find(id);
            if (prodct == null)
            {
                return HttpNotFound();
            }

            return View(prodct);
        }
        [HttpPost, ActionName("Edit_Category")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Category(tblproductcategory category, string procated,int? id)
        {

            if (ModelState.IsValid)
            {

                category.category_name = procated;


                db.tblproductcategories.Add(category);
                db.SaveChanges();


            }
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory prodct = db.tblproductcategories.Find(id);
            if (prodct == null)
            {
                return HttpNotFound();
            }

            return View(prodct);
        }
        public ActionResult Delete_Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory prodctdtv = db.tblproductcategories.Find(id);
            if (prodctdtv == null)
            {
                return HttpNotFound();
            }

            return View(prodctdtv);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Category(tblproductcategory category,int? id)
        {
            if (ModelState.IsValid)
            {



                category.flag = "0";
                db.tblproductcategories.Add(category);
                db.SaveChanges();


            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproductcategory prodctdt = db.tblproductcategories.Find(id);
            if (prodctdt == null)
            {
                return HttpNotFound();
            }

            return View(prodctdt);
        }
    }
}