using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Controllers
{
    public class HomeController : Controller
    {
        private HomeBudgetDBEntities context;

        public HomeController(HomeBudgetDBEntities context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            model.Context = this.context;

            if (PasswordAuthentication.UserIsValid(model))
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewData["LoginError"] = "User name or password was not correct. Please check your credentials and try again.";
            }            

            return View("Index");
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}