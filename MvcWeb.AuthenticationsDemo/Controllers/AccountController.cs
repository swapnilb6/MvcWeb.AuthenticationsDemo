using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWeb.AuthenticationsDemo.Models;
using System.Web.Security;

namespace MvcWeb.AuthenticationsDemo.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        { 
            using(var context = new EmployeeEntities())
            {
                bool isValid = context.tblUsers.Any(x => x.Username == model.Username && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username,false);
                    return RedirectToAction("Index","Employees");

                }
                ModelState.AddModelError("", "Invalid Username and or password");
                return View();
            }
           
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(tblUsers user)
        {
            using(var context = new EmployeeEntities())
            {
                context.tblUsers.Add(user);
                context.SaveChanges();
            }
            return RedirectToAction("Login");

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}