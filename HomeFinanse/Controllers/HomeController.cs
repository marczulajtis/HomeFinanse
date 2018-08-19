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
            this.mainViewModel = new MainViewModel(this.context);

            return View(this.mainViewModel);
        }


        [HttpGet]
        public ActionResult Summary()
        {
            return View(new MainViewModel(this.context));
        }

        [HttpPost]
        public ActionResult Summary(int SeletedPeriodID, int OnAccount, int PerMonth)
        {
            MainViewModel vm = new Models.MainViewModel(this.context);
            vm.PerMonth = PerMonth;
            vm.SelectedPeriodID = SeletedPeriodID.ToString();
            vm.OnAccount = OnAccount;

            return View(vm);
        }
    }
}