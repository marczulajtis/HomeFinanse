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

        public int SelectedPeriodID { get; private set; }

        [HttpGet]
        public ActionResult ShowIncomes()
        {
            this.SelectedPeriodID = Convert.ToInt32(Session["SelectedPeriodID"]);

            return View(new IncomeViewModel(this.context, new Income(), this.SelectedPeriodID));
        }
                
        public ActionResult AddIncome()
        {
            return View(new IncomeViewModel(this.context, new Income(), this.SelectedPeriodID));
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

            return PartialView("IncomesTable", this.context.Incomes);
        }

        [HttpDelete]
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
            }
            else
            {
                this.ModelState.AddModelError("", "No database data loaded.");
            }

            return PartialView("IncomesTable", this.context.Incomes);
        }

        [HttpGet]
        public ActionResult IncomesSummary()
        {
            return View(new MainViewModel(this.context, null));
        }

        public ActionResult UpdateIncomeDate(int incomeID)
        {
            var income = this.context?.Incomes?.Where(o => o.ID == incomeID).SingleOrDefault();

            try
            {
                income.IncomeDate = DateTime.Now;
                income.OnAccount = true;

                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("UpdateIncomeDateError", string.Format("Income date update error: {0}", ex.Message));
            }

            return View("IncomesTable", this.context?.Incomes?.Where(o => o.PeriodID == income.PeriodID));
        }

        /// <summary>
        /// Created new <see cref="Income"/> object from <see cref="IncomeViewModel"/> data
        /// </summary>
        /// <param name="newIncome">Return <see cref="Income"/> object</param>
        private Income CreateNewIncome(IncomeViewModel model)
        {
            Income newIncomeObject = new Income();

            Period incomePeriod = this.context.Periods.Where(x => x.PeriodID == model.NewIncome.PeriodID).SingleOrDefault();

            if (model.NewIncome != null)
            {
                newIncomeObject.IncomeName = model.NewIncome.IncomeName;
                newIncomeObject.Value = model.NewIncome.Value;
                newIncomeObject.IncomeDate = model.NewIncome.IncomeDate;
                newIncomeObject.Period = incomePeriod;
                newIncomeObject.OnAccount = model.NewIncome.OnAccount;
            }

            return newIncomeObject;
        }
    }
}