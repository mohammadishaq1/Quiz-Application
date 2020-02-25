using Quiz_Application.Models;
using Quiz_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Quiz_Application.Controllers
{
    public class AccountController : Controller
    {
        private QuizDBEntities objQuizDBEntities;

        public AccountController()
        {
            objQuizDBEntities = new QuizDBEntities();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index (AdminViewModel objadminViewModel)
        {
            if (ModelState.IsValid)
            {
                Admin obAdmin = objQuizDBEntities.Admins.SingleOrDefault(m => m.UserName == objadminViewModel.UserName);
                if (obAdmin == null)
                {
                    ModelState.AddModelError(string.Empty, "Email not exist");
                }
                else if(obAdmin.UserPassword != objadminViewModel.UserPassword)
                {
                    ModelState.AddModelError(string.Empty, "Email and Password is not valid");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(objadminViewModel.UserName, false);
                    var authTicket = new FormsAuthenticationTicket(1, obAdmin.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Admin");
                    string encryptTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}