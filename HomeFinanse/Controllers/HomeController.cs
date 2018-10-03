using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Controllers
{
    public class HomeController : Controller
    {
        private HomeBudgetDBEntities context;

        private MainViewModel mainViewModel;

        public HomeController()
        { }

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

            return View();
        }

        public ActionResult Dashboard()
        {
            this.mainViewModel = new MainViewModel(this.context, null);

            return View(this.mainViewModel);
        }
        
        [HttpGet]
        public void UpdateSelectedPeriod(int selectedPeriodID)
        {
            Session["SelectedPeriodID"] = selectedPeriodID;
        }


        [HttpGet]
        public ActionResult Summary(int selectedPeriodID)
        {
            return View(new MainViewModel(this.context, selectedPeriodID.ToString()));
        }

        [HttpPost]
        public ActionResult SetSelectedPeriodID(int selectedPeriodID)
        {
            Session["SelectedPeriodID"] = selectedPeriodID;

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Summary()
        {

            MainViewModel vm = new MainViewModel(this.context, Session["SelectedPeriodID"]?.ToString());
            return View(vm);
        }
    }
}