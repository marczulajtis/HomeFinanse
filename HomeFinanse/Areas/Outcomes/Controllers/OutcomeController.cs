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
        HomeBudgetDBEntities context;

        public OutcomeController(HomeBudgetDBEntities context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult ShowOutcomes()
        {
            return View(new OutcomeViewModel(context, new OutcomeNotNullable()));
        }

        public ActionResult AddOutcome()
        {
            // load lastly added outcome
            Outcome lastlyAddedOutcome = this.context.Outcomes.OrderByDescending(x => x.ID).First();

            if (lastlyAddedOutcome != null)
            {
                return View(new OutcomeViewModel(this.context, new OutcomeNotNullable {
                    ID = lastlyAddedOutcome.ID,
                    OutcomeName = lastlyAddedOutcome.OutcomeName,
                    Category = lastlyAddedOutcome.Category,
                    Period = lastlyAddedOutcome.Period,
                    Date = lastlyAddedOutcome.Date,
                    Place = lastlyAddedOutcome.Place,
                    Planned = lastlyAddedOutcome.Planned,
                    Value = lastlyAddedOutcome.Value,
                    Payed = lastlyAddedOutcome.Payed
                    //Year = lastlyAddedOutcome.Year
                    //Month = lastlyAddedOutcome.Month
                }));
            }

            return View(new OutcomeViewModel(this.context, new OutcomeNotNullable()));
        }

        [HttpPost]
        public ActionResult AddOutcome(OutcomeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        if (model.NewOutcome != null)
                        {
                            // add new income to db
                            this.context.Outcomes.Add(this.CreateNewOutcome(model));
                            this.context.SaveChanges();

                            this.ViewData["OutcomeAded"] = "Outcome successfully added.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                    this.ModelState.AddModelError("OutcomeAddingError", "Something went wrong when adding outcome. Try again.");
            }

            return View("ShowOutcomes", new OutcomeViewModel(this.context, new OutcomeNotNullable()));
        }

        private Outcome CreateNewOutcome(OutcomeViewModel model)
        {
            Outcome newOutcomeObject = new Outcome();

            Category outcomeCategory = this.context.Categories.Where(x => x.CategoryID == model.NewOutcome.CategoryID).SingleOrDefault();
            Period outcomePeriod = this.context.Periods.Where(x => x.PeriodID == model.NewOutcome.PeriodID).SingleOrDefault();

            if (model.NewOutcome != null)
            {
                newOutcomeObject.OutcomeName = model.NewOutcome.OutcomeName;
                newOutcomeObject.Value = model.NewOutcome.Value;
                newOutcomeObject.Date = model.NewOutcome.Date;
                newOutcomeObject.Place = model.NewOutcome.Place;
                newOutcomeObject.Category = outcomeCategory;
                newOutcomeObject.Planned = model.NewOutcome.PlannedNotNullable;
                newOutcomeObject.Period = outcomePeriod;
                newOutcomeObject.Payed = model.NewOutcome.Payed;
                //newOutcomeObject.Month = model.NewOutcome.Month;
                //newOutcomeObject.Year = model.NewOutcome.Year;
            }

            return newOutcomeObject;
        }

        public ActionResult DeleteOutcome(int outcomeID)
        {
            if (this.context != null)
            {
                Outcome outcomeToDelete = this.context.Outcomes.Where(outcome => outcome.ID == outcomeID).SingleOrDefault();

                if (outcomeToDelete != null)
                {
                    // delete income from database
                    this.context.Outcomes.Remove(outcomeToDelete);
                    this.context.SaveChanges();
                }

                return View("ShowOutcomes", new OutcomeViewModel(this.context, new Models.OutcomeNotNullable()));
            }

            this.ModelState.AddModelError("", "No database data loaded.");

            return View("ShowOutcomes");
        }
    }
}