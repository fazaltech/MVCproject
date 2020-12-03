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

       
        public ActionResult Index(string user_name, string roles)
        {

            var Lst = new List<string>();

            var Qry = from d in db.tblusers
                      orderby d.role
                      select d.role;
            Lst.AddRange(Qry.Distinct());
            ViewBag.roles = new SelectList(Lst);

            var urlt = from m in db.tblusers
                       select m;
            if (!String.IsNullOrEmpty(user_name))
            {
                urlt = urlt.Where(s => s.user_name.Contains(user_name));
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
        public ActionResult Registration(tbluser use,string first_name,string last_name,string email_address,string user_password,string repeat_password)
        {


            

            Thread.Sleep(200);
            var precheck = db.tblusers.Where(x => x.user_name == use.user_name).FirstOrDefault();
            var rdnum = new System.Random();
            int random = rdnum.Next(100);

            string dd= DateTime.Now.ToString("yyMMddhhmmss");
            decimal num = decimal.Parse(dd+random);


            if (precheck != null)
            {
                ViewBag.chk = "User Already Exist";
                return View(use);

            }
            else if (ModelState.IsValid)
            {
                use.user_name = first_name;
                use.email_id = email_address;
                use.fullname = first_name + last_name;
                use.user_id =num;
                use.password = user_password;

                use.role = "assign role";
                use.designation = "assign by admin";
                use.account_type = 0;
                use.flag = "0";

                use.role = "assign role";
                db.tblusers.Add(use);
                db.SaveChanges();


            }
            ViewBag.Message = "Contact admin to assign role and designation";
            return View();


        }
        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tbluser user,string Password, string Email)
        {
            user.email_id = Email;
            user.password = Password;

            var flag = db.tblusers
              .Where(x => x.email_id == user.email_id)
              .Where(x => x.password == user.password)
              .Select(x => x.flag).Max();
            string flnum = flag;


           
                if (IsValid(user.email_id, user.password) && flnum=="1")
                {
                    //var mail = db.tblusers
                    //  .Where(x => x.user_name == user.user_name)
                    //  .Where(x => x.password == user.password)
                    //  .Select(x => x.email_id).Max();
                    // string m = mail;



                    //Session["field"] = Fieldview.ToString();



                    FormsAuthentication.SetAuthCookie(user.email_id, false);


                    return RedirectToAction("Index", "Home");


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


        private bool IsValid(string email, string passwords)
        {

            bool IsValid = false;


            
            var user = db.tblusers.FirstOrDefault(u => u.email_id == email);
            string fl = user.flag;
            
            if (user != null)
            {
                if (user.password == passwords )
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