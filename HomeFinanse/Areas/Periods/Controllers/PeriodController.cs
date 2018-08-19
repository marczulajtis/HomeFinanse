using HomeFinanse.Areas.Periods.Models;
using HomeFinanse.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Periods.Controllers
{
    public class PeriodController : Controller
    {
        private HomeBudgetDBEntities context;
        private MainViewModel mainVM;
        private Period newPeriod;

        public PeriodController(HomeBudgetDBEntities context, MainViewModel mainVM)
        {
            this.context = context;
            this.mainVM = mainVM;
        }

        public ActionResult AddPeriod()
        {
            // load lastly added period
            Period lastlyAddedPeriod = this.context?.Periods?.OrderByDescending(x => x.PeriodID).First();

            if (lastlyAddedPeriod != null)
            {
                return View(new PeriodViewModel(this.context, new PeriodModel
                {
                    PeriodID = lastlyAddedPeriod.PeriodID,
                    PeriodName = lastlyAddedPeriod.PeriodName,
                    PeriodStart = lastlyAddedPeriod.PeriodStart,
                    PeriodEnd = lastlyAddedPeriod.PeriodEnd
                }));
            }

            return View(new PeriodViewModel(this.context, new PeriodModel()));
        }

        [HttpPost]
        public ActionResult AddPeriod(PeriodViewModel viewModel)
        {
            if (this.context != null)
            {
                if (this.context.Periods != null)
                {
                    if (!(this.PeriodAlreadyExists(viewModel.NewPeriod)))
                    {
                        this.context.Periods.Add(new Period {
                            PeriodEnd = ((DateTime)viewModel.NewPeriod.NullableEndDate),
                            PeriodStart = ((DateTime)viewModel.NewPeriod.NullableStartDate),
                            PeriodName = viewModel.NewPeriod.PeriodName
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

            return View("PeriodsTable", this.context.Periods);
        }

        private bool PeriodAlreadyExists(PeriodModel period)
        {
            var per = this.context?.Periods?.ToList().Where(x => x.PeriodName == period.PeriodName).SingleOrDefault();

            if (per != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public ActionResult ShowPeriods()
        {
            return View(new PeriodViewModel(this.context, new PeriodModel()));
        }

        [HttpDelete]
        public ActionResult DeletePeriod(int periodID)
        {
            if (this.context != null)
            {
                Period periodToDelete = this.context?.Periods?.Where(p => p.PeriodID == periodID).SingleOrDefault();

                if (periodToDelete != null)
                {
                    try
                    {
                        // delete category from database
                        this.context.Periods.Remove(periodToDelete);
                        this.context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError("DeletingPeriodException", 
                            "Could not delete period because it's assigned to another object like Income / Outcome");
                    }
                }
            }
            else
            {
                this.ModelState.AddModelError("", "No database data loaded.");
            }

            return PartialView("PeriodsTable", this.context.Periods);
        }

        public ActionResult SelectPeriod()
        {
            return PartialView("SelectPeriod", new PeriodViewModel(this.context, new PeriodModel()));
        }

        [HttpPost]
        public ActionResult SelectPeriod(PeriodViewModel periodVM)
        {
            Period selectedPeriod = this.context?.Periods?.Where(period => period.PeriodID == periodVM.SelectedPeriod.PeriodID).SingleOrDefault();
            this.mainVM.SelectedPeriod = selectedPeriod;   

            return View("~/Views/Home/Dashboard.cshtml");
        }
    }
}