using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Outcomes.Models
{
    public class OutcomeViewModel
    {
        private HomeBudgetDBEntities context;
        private OutcomeNotNullable newOutcome = new OutcomeNotNullable();
        private string selectedPeriodID;

        public OutcomeViewModel()
        { }

        public OutcomeViewModel(HomeBudgetDBEntities context, OutcomeNotNullable newOutcome, int newSelectedPeriodID)
        {
            this.context = context;
            this.newOutcome = newOutcome;
            this.SelectedPeriodID = newSelectedPeriodID;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OutcomeDate
        {
            get;
            set;
        }

        public List<Outcome> Outcomes
        {
            get
            {
                if (this.context != null)
                {
                    int periodID = Convert.ToInt32(this.SelectedPeriodID);

                    return this.context.Outcomes.Where(o => o.PeriodID == periodID).ToList();
                }

                return new List<Outcome>();
            }
        }

        public List<Category> Categories
        {
            get
            {
                if (this.context != null)
                {
                    return this.context.Categories.ToList();
                }

                return new List<Category>();
            }
        }

        public List<Period> Periods
        {
            get
            {
                if (this.context != null)
                {
                    return this.context.Periods.ToList();
                }

                return new List<Period>();
            }
        }

        public OutcomeNotNullable NewOutcome
        {
            get
            {
                return newOutcome;
            }

            set
            {
                newOutcome = value;
            }
        }

        public int SelectedPeriodID
        {
            get; set;

        }

        public List<SelectListItem> PeriodsSelectList
        {
            get { return this.GetPeriods(); }
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