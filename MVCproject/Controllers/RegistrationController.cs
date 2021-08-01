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

        //[Authorize(Roles = "admin")]
        public ActionResult Index(string user_name, string roles)
        {
            var adms = TempData["adminalert"];

            var Lst = new List<string>();

            var Qry = from d in db.tblusers
                      where d.flag == "1"
                      orderby d.role
                      select d.role;
            Lst.AddRange(Qry.Distinct());
            ViewBag.roles = new SelectList(Lst);

            var urlt = from m in db.tblusers where m.flag == "1"
                       select m;
            if (!String.IsNullOrEmpty(user_name))
            {
                urlt = urlt.Where(s => s.user_name.Contains(user_name));
            }
            if (!string.IsNullOrEmpty(roles))
            {
                urlt = urlt.Where(x => x.role == roles);
            }

            if (adms != null) {
                ViewBag.adminmeg = adms.ToString();
            }
            return View(urlt);

        }






        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(tbluser use, string first_name, string last_name, string email_address, string user_password, string repeat_password)
        {

            use.user_name = first_name;


            Thread.Sleep(200);
            var precheck = db.tblusers.Where(x => x.user_name == use.user_name).FirstOrDefault();
            var rdnum = new System.Random();
            int random = rdnum.Next(100);

            string dd = DateTime.Now.ToString("yyMMddhhmmss");
            decimal num = decimal.Parse(dd + random);


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
                use.user_id = num;
                use.password = user_password;

                use.role = "assign role";
                use.designation = "assign by admin";
                use.account_type = 0;
                use.flag = "1";

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
        public ActionResult Login(tbluser user, string Password, string Email)
        {
            user.email_id = Email;
            user.password = Password;

            var flag = db.tblusers
              .Where(x => x.email_id == user.email_id)
              .Where(x => x.password == user.password)
              .Select(x => x.flag).Max();
            string flnum = flag;



            if (IsValid(user.email_id, user.password) && flnum == "1")
            {

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


            if (user != null)
            {
                if (user.password == passwords)
                {
                    IsValid = true;
                }
            }

            return IsValid;
        }


        // [Authorize(Roles = "admin")]
        public ActionResult AssignRole(int? id)
        {

            var adminidchk = db.tblusers.Where(x => x.id == id).Select(x => x.user_name).Max();
            string admin = adminidchk;

            if (admin == "admin")
            {
                TempData["adminalert"] = "Admin User can not change";
                return RedirectToAction("Index", "Registration");

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
            try {
                TempData["adminalert"] = null;
                var data = (from d in db.tblusers
                            where d.id == id
                            where d.flag == "1"

                            select new
                            {
                                d.id,
                                d.user_name,
                                d.fullname,
                                d.email_id,
                                d.designation,
                                d.role

                            }).ToList();



                TempData["emprole"] = data;
                return View(urse);
            }

            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }

        }



        [HttpPost, ActionName("AssignRole")]
        // [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole([Bind(Include = "Id,user_name,email_id,password,role")] Models.tbluser users, string Roles, int? id)
        {



            if (Roles == null)
            {

                return Json(new { success = false, responseText = "Select Role Correctly" }, JsonRequestBehavior.AllowGet);


            }
            if (ModelState.IsValid)
            {
                var result = db.tblusers.SingleOrDefault(b => b.id == id);

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
        [HttpGet]
        public JsonResult Emproleview()
        {
            var data = TempData["emprole"];


            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add_Role()
        {
            return View();
        }
        [HttpPost, ActionName("Add_Role")]
        // [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Role(tblroles role, string vrole)
        {
            role.role = vrole;
            var precheck = db.tblrole.Where(x => x.role == role.role).FirstOrDefault();

            if (precheck != null)
            {
                ViewBag.chk = "Role Already Exist";
                return View(role);

            }

            if (ModelState.IsValid)
            {
                role.role = vrole;
                role.flag = "1";
                db.tblrole.Add(role);
                db.SaveChanges();
            }
            return Json(new { success = true, responseText = "Role Added" }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Role_Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult RoleView()
        {
            try
            {
                var pro = (from d in db.tblrole
                           where d.flag == "1"
                           select new
                           {
                               d.id,
                               d.role,


                           }).ToList();
                return Json(new { data = pro }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }


        public ActionResult Role_Edit(tblroles role)
        {
            return View();
        }


        [HttpGet]
        public JsonResult Assg_Role()
        {


            try
            {
                return Json(db.tblrole.Where(x => x.flag == "1").Select(x => new
                {
                    Roleid = x.id,
                    RoleName = x.role
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }
        }

        public ActionResult User_Edit(int? id)
        {

            var adminidchk = db.tblusers.Where(x => x.id == id).Select(x => x.user_name).Max();
            string admin = adminidchk;

            if (admin == "admin")
            {
                TempData["adminalert"] = "Admin User can not change";
                return RedirectToAction("Index", "Registration");

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
            try
            {
                TempData["adminalert"] = null;
                var data = (from d in db.tblusers
                            where d.id == id
                            where d.flag == "1"

                            select new
                            {
                                d.id,
                                d.user_name,
                                d.fullname,
                                d.email_id,
                                d.designation,
                                d.role

                            }).ToList();



                TempData["useredit"] = data;
                return View(urse);
            }

            catch (Exception ex)
            {
                return ViewBag.error = ex.Message;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User_Edit(int? id , user_view userview , user_ed useud) {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usr = (from d in db.tblusers
                       where d.id == id
                       select new
                       {
                           d.id,
                           d.user_name,
                           d.fullname,
                           d.designation,
                           d.email_id
                          
                       });


            if (userview.username_ed != null)
            {
                useud.username_ud = userview.username_ed;
            }
            else 
            {
                useud.username_ud = usr.Select(x => x.user_name).Max();
            }


            if (userview.fullname_ed != null)
            {
                useud.fullname_ud = userview.fullname_ed;
            }
            else
            {
                useud.fullname_ud = usr.Select(x => x.fullname).Max();
            }


            if (userview.email_ed != null)
            {
                useud.email_ud = userview.email_ed;
            }
            else
            {
                useud.email_ud = usr.Select(x => x.email_id).Max();
            }

            if (userview.degisnation_ed != null)
            {
                useud.degisnation_ud = userview.degisnation_ed;
            }
            else
            {
                useud.degisnation_ud = usr.Select(x => x.designation).Max();
            }


            if (ModelState.IsValid)
            {
                var userupd = db.tblusers.SingleOrDefault(b => b.id == id);

                userupd.user_name = useud.username_ud;
                userupd.fullname = useud.fullname_ud;
                userupd.email_id = useud.email_ud;
                userupd.designation = useud.degisnation_ud;
                db.SaveChanges();


                return Json(new { success = true, responseText = "Record Update" }, JsonRequestBehavior.AllowGet);
            }

           
            tbluser urse = db.tblusers.Find(id);
            if (urse == null)
            {
                return HttpNotFound();
            }

            return View(urse);
        }

        [HttpGet]
        public JsonResult EditUser_View()
        {
            var data = TempData["useredit"];


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Del_User(int? id)
        {
            return View();
        }


    }



    public class user_view
    {

        public string username_ed { get; set; }
        public string fullname_ed { get; set; }
        public string email_ed { get; set; }
        public string degisnation_ed { get; set; }
    }


    public class user_ed
    {

        public string username_ud { get; set; }
        public string fullname_ud { get; set; }
        public string email_ud { get; set; }
        public string degisnation_ud { get; set; }
    }
}