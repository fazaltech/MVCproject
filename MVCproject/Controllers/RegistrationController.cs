using MVCproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCproject.Controllers
{
    public class RegistrationController : Controller
    {

        private mvc_pos_conn db = new mvc_pos_conn();

        // GET: Registration
        // GET: Registration

        [Authorize(Roles = "admin")]
        public ActionResult Index(string name, string roles)
        {

            var Lst = new List<string>();

            var Qry = from d in db.tblusers
                      orderby d.role
                      select d.role;
            Lst.AddRange(Qry.Distinct());
            ViewBag.roles = new SelectList(Lst);

            var urlt = from m in db.tblusers
                       select m;
            if (!String.IsNullOrEmpty(name))
            {
                urlt = urlt.Where(s => s.user_name.Contains(name));
            }
            if (!string.IsNullOrEmpty(roles))
            {
                urlt = urlt.Where(x => x.role == roles);
            }



            return View(urlt);

        }
        [HttpGet]
        public ActionResult Jsonindex(int? id, string roles)
        {



            var urlt = from m in db.tblusers
                       select m;
            if (id != null)
            {
                urlt = urlt.Where(s => s.user_id == id);
            }

            if (!string.IsNullOrEmpty(roles))
            {
                urlt = urlt.Where(s => s.role == roles);
            }

            return View(urlt);

        }

        [HttpPost, ActionName("Jsonindex")]
        [ValidateAntiForgeryToken]
        public ActionResult Jsonindex([Bind(Include = "Id,user_name,email_id,password,role")]Models.tbluser users, string Roles, int? id)
        {
            if (ModelState.IsValid)
            {
                var result = db.tblusers.SingleOrDefault(b => b.user_id == id);

                result.role = Roles;
                db.SaveChanges();

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbluser urse = db.tblusers.Find(id);
            if (urse == null)
            {
                return HttpNotFound();
            }

            return View(urse);
        }





        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(tbluser use)
        {

            Thread.Sleep(200);
            var precheck = db.tblusers.Where(x => x.user_name == use.user_name).FirstOrDefault();

            if (precheck != null)
            {
                ViewBag.chk = "User Already Exist";
                return View(use);

            }
            else if (ModelState.IsValid)
            {

                use.role = "assign role";
                db.tblusers.Add(use);
                db.SaveChanges();


            }
            ViewBag.Message = "Contact admin to assign role";
            return View();


        }
        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tbluser user, string Fieldview)
        {
            var assgrole = db.tblusers
                 .Where(x => x.user_name == user.user_name)
                 .Where(x => x.password == user.password)
                 .Select(x => x.role).Max();
            if (Fieldview == "-1")
            {
                ViewBag.FieldSelect = "Please Select a Field";
                return View(user);
            }

            else if (assgrole == "assign role")
            {
                ViewBag.assgnRole = "Contact admin to assign role";
                return View(user);

            }

            else if (IsValid(user.user_name, user.password))
            {
                var mail = db.tblusers
                  .Where(x => x.user_name == user.user_name)
                  .Where(x => x.password == user.password)
                  .Select(x => x.email_id).Max();
                string m = mail;



                Session["field"] = Fieldview.ToString();



                FormsAuthentication.SetAuthCookie(user.user_name, false);


                return RedirectToAction("ReportEorror", "Feedback");


            }
            else
            {


                ViewBag.Mg = "Login details are wrong.";
                return View(user);

            }


        }


        public ActionResult Logout(string returnUrl = null)
        {

            FormsAuthentication.SignOut();

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Login", "Registration");

            return Redirect(returnUrl);
        }


        private bool IsValid(string name, string passwords)
        {

            bool IsValid = false;


            var user = db.tblusers.FirstOrDefault(u => u.user_name == name);
            if (user != null)
            {
                if (user.password == passwords)
                {
                    IsValid = true;
                }
            }

            return IsValid;
        }

        //public JsonResult Fields()
        //{
        //    try
        //    {
        //        return Json(db..Select(x => new
        //        {
        //            DepartmentID = x.Id,
        //            DepartmentName = x.field_name
        //        }).ToList(), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ViewBag.error = ex.Message;
        //    }
        //}
        [Authorize(Roles = "admin")]
        public ActionResult AssignRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbluser urse = db.tblusers.Find(id);
            if (urse == null)
            {
                return HttpNotFound();
            }

            return View(urse);
        }
        [HttpPost, ActionName("AssignRole")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole([Bind(Include = "Id,user_name,email_id,password,role")]Models.tbluser users, string Roles, int? id)
        {
            if (ModelState.IsValid)
            {
                var result = db.tblusers.SingleOrDefault(b => b.user_id == id);

                result.role = Roles;
                db.SaveChanges();

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbluser urse = db.tblusers.Find(id);
            if (urse == null)
            {
                return HttpNotFound();
            }

            return View(urse);
        }

        public ActionResult JsAssignRole([Bind(Include = "Id,user_name,email_id,password,role")]Models.tbluser users, string Roles, int? id)
        {
            if (ModelState.IsValid)
            {
                var result = db.tblusers.SingleOrDefault(b => b.user_id == id);

                result.role = Roles;
                db.SaveChanges();

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbluser urse = db.tblusers.Find(id);
            if (urse == null)
            {
                return HttpNotFound();
            }

            return View(urse);
        }



    }
}