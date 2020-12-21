using MVCproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVCproject.Controllers
{
    public class Product_Category_Controller : Controller
    {
        private mvc_pos_conn db = new mvc_pos_conn();
        // GET: Product_Category_
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Add_Category()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Category(tblproductcategory category,string procat)
        {

            //category.category_name = procat;


            //Thread.Sleep(200);
            //var precheck = db.tblproductcategories.Where(x => x.category_name == category.category_name).FirstOrDefault();
            //var rdnum = new System.Random();
            //int random = rdnum.Next(100);

            //string dd = DateTime.Now.ToString("yyMMddhhmmss");
            //string catid = "pcid"+dd + random;


            //if (precheck != null)
            //{
            //    ViewBag.chk = "User Already Exist";
            //    return View(use);

            //}
            //else if (ModelState.IsValid)
            //{
               

               
            //    db.tblusers.Add(use);
            //    db.SaveChanges();


            //}
            //ViewBag.Message = "Contact admin to assign role and designation";
            return View();


        }


    }
}