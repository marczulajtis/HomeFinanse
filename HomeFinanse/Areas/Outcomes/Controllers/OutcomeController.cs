using HomeFinanse.Areas.Outcomes.Models;
using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Outcomes.Controllers
{
    public class OutcomeController : Controller
    {
        private HomeBudgetDBEntities context;
        
        public OutcomeController(HomeBudgetDBEntities context)
        {
            this.context = context;
        }

        public int SelectedPeriodID => Convert.ToInt32(Session["SelectedPeriodID"]);

        [HttpGet]
        public ActionResult ShowOutcomes()
        {
            return PartialView(new OutcomeViewModel(context, new Outcome(), this.SelectedPeriodID));
        }

        public ActionResult AddOutcome()
        {
            // load lastly added outcome
            Outcome lastlyAddedOutcome = this.context?.Outcomes?.OrderByDescending(x => x.ID).First();

            if (lastlyAddedOutcome != null)
            {
                return View(new OutcomeViewModel(this.context, new Outcome {
                    ID = lastlyAddedOutcome.ID,
                    OutcomeName = lastlyAddedOutcome.OutcomeName,
                    Category = lastlyAddedOutcome.Category,
                    Period = lastlyAddedOutcome.Period,
                    Date = lastlyAddedOutcome.Date,
                    Place = lastlyAddedOutcome.Place,
                    Planned = lastlyAddedOutcome.Planned,
                    Value = lastlyAddedOutcome.Value,
                    Payed = lastlyAddedOutcome.Payed
                }, this.SelectedPeriodID));
            }

            return PartialView(new OutcomeViewModel(this.context, new Outcome(), this.SelectedPeriodID));
        }

        [HttpPost]
        public ActionResult AddOutcome(OutcomeViewModel model)
        {
            try
            {
                if (model != null)
                {
                    if (model.NewOutcome != null)
                    {
                        model.NewOutcome.PeriodID = this.SelectedPeriodID;

                        // add new income to db
                        this.context?.Outcomes?.Add(this.CreateNewOutcome(model));
                        this.context?.SaveChanges();

                        this.ViewData["OutcomeAded"] = "Outcome successfully added.";
                    }
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("OutcomeAddingError", "Something went wrong when adding outcome. Try again.");
            }

            return PartialView("OutcomesTable", this.context?.Outcomes.Where(o => o.PeriodID == model.NewOutcome.PeriodID));
        }

        private Outcome CreateNewOutcome(OutcomeViewModel model)
        {
            Outcome newOutcomeObject = new Outcome();

            Category outcomeCategory = this.context?.Categories?.Where(x => x.CategoryID == model.NewOutcome.CategoryID).SingleOrDefault();
            Period outcomePeriod = this.context?.Periods?.Where(x => x.PeriodID == model.NewOutcome.PeriodID).SingleOrDefault();

            if (model.NewOutcome != null)
            {
                newOutcomeObject.OutcomeName = model.NewOutcome.OutcomeName;
                newOutcomeObject.Value = model.NewOutcome.Value;
                newOutcomeObject.Date = model.NewOutcome.Date;
                newOutcomeObject.Place = model.NewOutcome.Place;
                newOutcomeObject.Category = outcomeCategory;
                newOutcomeObject.Planned = model.NewOutcome.Planned;
                newOutcomeObject.Period = outcomePeriod;
                newOutcomeObject.Payed = model.NewOutcome.Payed;
            }

            return newOutcomeObject;
        }

        [HttpDelete]
        public ActionResult DeleteOutcome(int outcomeID)
        {
            Outcome outcomeToDelete = new Outcome();

            if (this.context != null)
            {
                outcomeToDelete = this.context?.Outcomes?.Where(outcome => outcome.ID == outcomeID).SingleOrDefault();

                if (outcomeToDelete != null)
                {
                    // delete income from database
                    this.context?.Outcomes?.Remove(outcomeToDelete);
                    this.context?.SaveChanges();
                }
            }
            else
            {
                this.ModelState.AddModelError("", "No database data loaded.");
            }

            // outcomes for curently selected period
            var outcomes = this.context?.Outcomes?.Where(o => o.PeriodID == outcomeToDelete.PeriodID);

            return PartialView("OutcomesTable", outcomes);
        }

        public ActionResult UpdateOutcomeDate(int outcomeID)
        {
            var outcome = this.context?.Outcomes?.Where(o => o.ID == outcomeID).SingleOrDefault();

            try
            {
                outcome.Date = DateTime.Now;
                outcome.Payed = true;

                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("UpdateOutcomeDateError", string.Format("Outcome date update error: {0}", ex.Message));
            }

            return PartialView("OutcomesTable", this.context?.Outcomes?.Where(o => o.PeriodID == outcome.PeriodID));
        }

        public static bool DateNowIsBetween(DateTime date1, DateTime date2)
        {
            return (DateTime.Now >= date1 && DateTime.Now <= date2);
        }
        
        [HttpGet]
        public ActionResult EditOutcome(int outcomeId)
        {
            var outcome = context?.Outcomes?.Where(o => o.ID == outcomeId).SingleOrDefault();

            var outcomeViewModel = new OutcomeViewModel(this.context, outcome, Convert.ToInt32(Session["SelectedPeriodID"]));

            return PartialView("EditOutcome", outcomeViewModel);
        }
        
        [HttpPost]
        public ActionResult UpdateOutcome(OutcomeViewModel vm)
        {
            try
            {
                var outcomeToUpdate = this.context.Outcomes.Where(o => o.ID == vm.NewOutcome.ID).SingleOrDefault();

                outcomeToUpdate.CategoryID = vm.NewOutcome.CategoryID;
                outcomeToUpdate.Date = vm.NewOutcome.Date;
                outcomeToUpdate.OutcomeName = vm.NewOutcome.OutcomeName;
                outcomeToUpdate.Payed = vm.NewOutcome.Payed;
                outcomeToUpdate.PeriodID = vm.NewOutcome.PeriodID;
                outcomeToUpdate.Planned = vm.NewOutcome.Planned;
                outcomeToUpdate.Value = vm.NewOutcome.Value;

                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = "Could not update outcome. Try again.";
            }

            return PartialView("OutcomesTable", this.context?.Outcomes.Where(o => o.PeriodID == this.SelectedPeriodID));
        }
    }
}