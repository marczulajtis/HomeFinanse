using HomeFinanse.Areas.Periods.Models;
using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Periods.Controllers
{
    public class PeriodController : Controller
    {
        private HomeBudgetDBEntities context;

        public PeriodController(HomeBudgetDBEntities context)
        {
            this.context = context;
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

            return PartialView(new PeriodViewModel(this.context, new PeriodModel()));
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

                        var lastPeriodID = this.context?.Periods?.ToList()?.LastOrDefault()?.PeriodID;

                        this.context.SaveChanges();

                        this.ViewData["PeriodAdded"] = "Period successfully added.";
                        
                        // Add incomes and planned outcomes from last period
                        var lastPeriodIncomes = this.context?.Incomes?.Where(i => i.PeriodID == lastPeriodID);

                        foreach (var income in lastPeriodIncomes)
                        {
                            this.context?.Incomes?.Add(new Income
                            {
                                IncomeDate = DateTime.Now,
                                IncomeName = income.IncomeName,
                                OnAccount = false,
                                PeriodID = this.context.Periods.ToList().LastOrDefault().PeriodID,
                                Value = income.Value
                            });
                        }

                        var lastPeriodOutcomes = this.context?.Outcomes?.Where(o => o.PeriodID == lastPeriodID && o.Planned == true);

                        foreach (var outcome in lastPeriodOutcomes)
                        {
                            this.context?.Outcomes?.Add(new Outcome
                            {
                                CategoryID = outcome.CategoryID,
                                OutcomeName = outcome.OutcomeName,
                                Payed = false,
                                PeriodID = this.context.Periods.ToList().LastOrDefault().PeriodID,
                                Place = outcome.Place,
                                Planned = true,
                                Value = outcome.Value,
                                Date = null
                            });
                        }

                        this.context.SaveChanges();
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
            return PartialView(new PeriodViewModel(this.context, new PeriodModel()));
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
                        // recursive delete
                        this.context.Outcomes.RemoveRange(this.context.Outcomes.Where(o => o.PeriodID == periodID));
                        this.context.Incomes.RemoveRange(this.context.Incomes.Where(o => o.PeriodID == periodID));

                        this.context.Periods.Remove(periodToDelete);
                        this.context.SaveChanges();

                        this.ViewBag.Periods = this.GetPeriods();
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

            return View("~/Views/Home/Dashboard.cshtml");
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