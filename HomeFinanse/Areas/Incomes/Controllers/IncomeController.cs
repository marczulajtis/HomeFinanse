using HomeFinanse.Areas.Incomes.Models;
using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Incomes.Controllers
{
    public class IncomeController : Controller
    {
        private HomeBudgetDBEntities context;

        public IncomeController(HomeBudgetDBEntities context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult ShowIncomes()
        {
            return View(new IncomeViewModel(this.context, new Income()));
        }
                
        public ActionResult AddIncome()
        {
            return View(new IncomeViewModel(this.context, new Income()));
        }

        [HttpPost]
        public ActionResult AddIncome(IncomeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        if (model.NewIncome != null)
                        {
                            // add new income to db
                            this.context.Incomes.Add(this.CreateNewIncome(model));
                            this.context.SaveChanges();

                            this.ViewData["IncomeAded"] = "Income successfully added.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("IncomeAddingError", "Something went wrong when adding income. Try again.");
            }

            return View("ShowIncomes", new IncomeViewModel(this.context, new Income()));
        }

        public ActionResult DeleteIncome(int incomeID)
        {
            if (this.context != null)
            {
                Income incomeToDelete = this.context.Incomes.Where(income => income.ID == incomeID).SingleOrDefault();

                if (incomeToDelete != null)
                {
                    // delete income from database
                    this.context.Incomes.Remove(incomeToDelete);
                    this.context.SaveChanges();
                }

                return View("ShowIncomes", new IncomeViewModel(this.context, new Income()));
            }

            this.ModelState.AddModelError("", "No database data loaded.");

            return View("ShowIncomes");
        }

        /// <summary>
        /// Created new <see cref="Income"/> object from <see cref="IncomeViewModel"/> data
        /// </summary>
        /// <param name="newIncome">Return <see cref="Income"/> object</param>
        private Income CreateNewIncome(IncomeViewModel model)
        {
            Income newIncomeObject = new Income();

            Category incomeCategory = this.context.Categories.Where(x => x.CategoryID == model.NewIncome.CategoryID).SingleOrDefault();
            Period incomePeriod = this.context.Periods.Where(x => x.PeriodID == model.NewIncome.PeriodID).SingleOrDefault();

            if (model.NewIncome != null)
            {
                newIncomeObject.IncomeName = model.NewIncome.IncomeName;
                newIncomeObject.Value = model.NewIncome.Value;
                newIncomeObject.IncomeDate = model.NewIncome.IncomeDate;
                newIncomeObject.Place = model.NewIncome.Place;
                newIncomeObject.Category = incomeCategory;
                newIncomeObject.Period = incomePeriod;
                newIncomeObject.OnAccount = model.NewIncome.OnAccount;
            }

            return newIncomeObject;
        }
    }
}