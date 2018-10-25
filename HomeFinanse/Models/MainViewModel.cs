using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeFinanse.Models
{
    public class MainViewModel
    {
        private HomeBudgetDBEntities1 dbContext;

        private string selectedPeriodID;

        private int onAccount;
        private int shouldBe;

        public MainViewModel(HomeBudgetDBEntities1 context, string selectedPeriodIDParam)
        {
            dbContext = context;
            this.SelectedPeriodID = selectedPeriodIDParam != null ? selectedPeriodIDParam : context?.Periods?.FirstOrDefault().PeriodID.ToString();
        }
        
        public decimal Difference
        {
            get { return this.ShouldBe - this.onAccount; } 
        }

        public int OnAccount
        {
            get { return this.onAccount; }
            set { this.onAccount = value; }
        }

        public decimal ShouldBe
        {
            get
            {
                return this.TotalIncomeOnAccount - this.TotalPayedOutcome;
            }
        }

        public List<Outcomes_by_category> OutcomesByCategoryView
        {
            get { return this.GetOutcomesByCategoryView(); }
        }

        public string SelectedPeriodID
        {
            get { return this.selectedPeriodID; }
            set
            {
                if (value == null || value == string.Empty)
                    this.selectedPeriodID = dbContext?.Periods?.FirstOrDefault()?.PeriodID.ToString();
                else
                    this.selectedPeriodID = value;
            }
        }

        public Period SelectedPeriod
        {
            get;
            set;
        }

        public List<SelectListItem> Periods
        {
            get { return this.GetPeriods(); }
        }

        public decimal TotalOutcome
        {
            get { return this.GetTotalOutcome(); }
        }

        public decimal TotalIncome
        {
            get { return this.GetTotalIncome(); }
        }

        public int PerMonth
        {
            get; set;
        }

        public decimal TotalIncomeOnAccount
        {
            get
            {
                int periodID = Convert.ToInt32(this.selectedPeriodID);

                var incomes = dbContext?.Incomes?.Where(i => i.PeriodID == periodID && i.OnAccount == true);

                var incomesTotal = incomes.Count() > 0 ? incomes.Sum(income => income.Value) : 0;

                return Convert.ToDecimal(incomesTotal);
            }
        }

        public decimal TotalPayedOutcome
        {
            get
            {
                int periodID = Convert.ToInt32(this.selectedPeriodID);

                var payedOutcomes = dbContext?.Outcomes?.Where(outcome => outcome.Payed == true && outcome.PeriodID == periodID);

                var totalPayedOutcome = payedOutcomes.Count() > 0 ? payedOutcomes.Sum(outcome => outcome.Value) : 0;

                return totalPayedOutcome;
            }
        }

        private decimal GetTotalIncome()
        {
            int periodID = Convert.ToInt32(this.selectedPeriodID);
            
            var incomes = dbContext?.Incomes?.Where(i => i.PeriodID == periodID);
            
            var incomesTotal = incomes.Count() > 0 ? incomes.Sum(income => income.Value) : 0;

            return Convert.ToDecimal(incomesTotal);
        }

        private decimal GetTotalOutcome()
        {
            int periodID = Convert.ToInt32(this.selectedPeriodID);

            var outcomes = dbContext?.Outcomes?.Where(o => o.PeriodID == periodID);

            var outcomesTotal = outcomes.Count() > 0 ? outcomes.Sum(outcome => outcome.Value) : 0;

            return Convert.ToDecimal(outcomesTotal);
        }

        private List<Outcomes_by_category> GetOutcomesByCategoryView()
        {
            int selectedPeriodToInt = Convert.ToInt32(this.selectedPeriodID);

            return dbContext?.Outcomes_by_category?.Where(outcome => outcome.PeriodID == selectedPeriodToInt).ToList();
        }

        private List<SelectListItem> GetPeriods()
        {
            var list = new List<SelectListItem>();

            if (dbContext?.Periods != null)
            {
                foreach (var period in dbContext?.Periods)
                {
                    list.Add(new SelectListItem { Text = period.PeriodName, Value = period.PeriodID.ToString() });
                }
            }

            return list;
        }
    }
}