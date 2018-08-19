using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeFinanse.Models
{
    public class MainViewModel
    {
        private static HomeBudgetDBEntities dbContext;

        private string selectedPeriodID;

        private int onAccount;
        private int shouldBe;

        public MainViewModel(HomeBudgetDBEntities context)
        {
            dbContext = context;
            this.SelectedPeriodID = context?.Periods?.FirstOrDefault()?.PeriodID.ToString();
        }

        public Summary CurrentSummary
        {
            get { return this.GetCurrentSummary(); }
        }

        public int Difference
        {
            get { return this.ShouldBe - this.onAccount; } 
        }

        public int OnAccount
        {
            get { return this.onAccount; }
            set { this.onAccount = value; }
        }

        public int ShouldBe
        {
            get {
                return (int)this.TotalIncome - this.TotalPayedOutcome; }
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

        public int TotalOutcome
        {
            get { return this.GetTotalOutcome(); }
        }

        public int TotalIncome
        {
            get { return this.GetTotalIncome(); }
        }

        public int PerMonth
        {
            get; set;
        }

        public int TotalPayedOutcome
        {
            get { return (int)dbContext?.Outcomes?.Where(outcome => outcome.Payed == true)?.Sum(outcome => outcome.Value); }
        }

        private int GetTotalIncome()
        {
            var incomesTotal = dbContext?.Incomes?.Where(i => i.OnAccount == true)?.Sum(income => (decimal?)income.Value);

            if (incomesTotal != null)
                return (int)incomesTotal;

            return 0;
        }

        private int GetTotalOutcome()
        {
            var outcomesTotal = dbContext?.Outcomes?.Sum(outcome => (decimal?)outcome.Value);

            if (outcomesTotal != null)
                return (int)outcomesTotal;

            return 0;
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

        private Summary GetCurrentSummary()
        {
            int periodID = Convert.ToInt32(this.selectedPeriodID);

            return dbContext?.Summaries?.Where(sum => sum.PeriodID == periodID).SingleOrDefault();
        }
    }
}