using ShauliBlog.Code;
using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShauliBlog.Controllers {
    [Authorize]
    public class LoginController : Controller {

        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return RedirectToAction("Create", "FansClub");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model) {

            if (ModelState.IsValid && AccountHelper.Login(model.UserName, model.Password))
            {
                // authenticate the user and redirect to home page
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Blog");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            AccountHelper.LogOff();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Login");
        }

    }
}