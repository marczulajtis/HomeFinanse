using HomeFinanse.Areas.Periods.Models;
using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Periods.Controllers
{
    public class PeriodController : Controller
    {
        private HomeBudgetDBEntities context;
        private MainViewModel mainVM;

        public PeriodController(HomeBudgetDBEntities context, MainViewModel mainVM)
        {
            this.context = context;
            this.mainVM = mainVM;
        }

        public ActionResult AddPeriod()
        {
            return PartialView("AddPeriod", new PeriodModel());
        }

        [HttpPost]
        public ActionResult AddPeriod(PeriodModel newPeriod)
        {
            if (this.context != null)
            {
                if (this.context.Periods != null)
                {
                    if (!(this.PeriodAlreadyExists(newPeriod)))
                    {
                        this.context.Periods.Add(new Period {
                            PeriodEnd = ((DateTime)newPeriod.NullableEndDate),
                            PeriodStart = ((DateTime)newPeriod.NullableStartDate),
                            PeriodName = newPeriod.PeriodName
                        });

                        this.context.SaveChanges();

                        this.ViewData["PeriodAdded"] = "Period successfully added.";
                    }
                    else
                    {
                        this.ModelState.AddModelError("PeriodExists", "This period already exists.");
                    }
                }
            }

            return PartialView("PeriodsTable", this.context.Periods);
        }

        private bool PeriodAlreadyExists(PeriodModel period)
        {
            var per = this.context.Periods.ToList().Where(x => x.PeriodName == period.PeriodName).SingleOrDefault();

            if (per != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public ActionResult ShowPeriods()
        {
            return View(this.context.Periods.ToList());
        }

        public ActionResult DeletePeriod(int periodID)
        {
            if (this.context != null)
            {
                Period periodToDelete = this.context.Periods.Where(cat => cat.PeriodID == periodID).SingleOrDefault();

                if (periodToDelete != null)
                {
                    // delete category from database
                    this.context.Periods.Remove(periodToDelete);
                    this.context.SaveChanges();
                }

                return View("ShowPeriods", this.context.Periods.ToList());
            }

            this.ModelState.AddModelError("", "No database data loaded.");

            return View("ShowPeriods");
        }

        public ActionResult SelectPeriod()
        {
            return PartialView("SelectPeriod", new PeriodViewModel(this.context));
        }

        [HttpPost]
        public ActionResult SelectPeriod(PeriodViewModel periodVM)
        {
            Period selectedPeriod = this.context.Periods.Where(period => period.PeriodID == periodVM.SelectedPeriod.PeriodID).SingleOrDefault();
            this.mainVM.SelectedPeriod = selectedPeriod;   

            return View("~/Views/Home/Dashboard.cshtml");
        }
    }
}