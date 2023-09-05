using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using FootballLeagueManagmentSystem.Models;
using System.Web.Security;

namespace FootballLeagueManagmentSystem.Controllers
{
   
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
            using(var context=new efootballLeagueEntities1())
            {
                bool isValid= context.Users.Any(x=>x.UserName==model.UserName && x.Password==model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,false);
                    return RedirectToAction("List","Teams");
                }
                ModelState.AddModelError("", "Invalid Details");
                return View();
            }
            
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Users model)
        {
            using (var context = new efootballLeagueEntities1())
            {
                context.Users.Add(model);
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