using HomeFinanse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            this.ViewBag.Periods = this.GetPeriods();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Login"] != null)
            {
                return RedirectToAction("Dashboard", new { userName = Session["Login"].ToString() });
            }

            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            model.Context = this.context;

            if (PasswordAuthentication.UserIsValid(model))
            {
                Session["Login"] = model?.LoginUser;

                if (Session["Login"] == null)
                {
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Dashboard", new { userName = Session["Login"].ToString() });
            }
            else
            {
                ViewData["LoginError"] = "User name or password was not correct. Please check your credentials and try again.";
            }            

            return View();
        }

        public ActionResult Dashboard(string userName)
        {
            this.mainViewModel = new MainViewModel(this.context, Session["SelectedPeriodID"]?.ToString());

            ViewBag.Periods = this.GetPeriods();

            if (Session["Login"] != null)
                if (Session["Login"].ToString() == userName)
                    return View(this.mainViewModel);

            return RedirectToAction("Login");
        }
        
        public ActionResult LogOut(UserViewModel model)
        {
            Session["Login"] = null;

            return RedirectToAction("Login");
        }

        [HttpGet]
        public void UpdateSelectedPeriod(int selectedPeriodID)
        {
            Session["SelectedPeriodID"] = selectedPeriodID;
        }


        [HttpGet]
        public ActionResult Summary(int selectedPeriodID)
        {
            return PartialView(new MainViewModel(this.context, selectedPeriodID.ToString()));
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
            return PartialView(vm);
        }

        [HttpGet]
        public ActionResult RefreshPeriods()
        {
            return Json( new { periods = new SelectList(this.GetPeriods(), "Value", "Text") }, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> GetPeriods()
        {
            var list = new List<SelectListItem>();

            if (context?.Periods != null)
            {
                foreach (var period in context?.Periods)
                {
                    list.Add(new SelectListItem { Text = period.PeriodName, Value = period.PeriodID.ToString() });
                }
            }

            return list;
        }
    }
}