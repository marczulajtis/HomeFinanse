using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Incomes.Models
{
    public class IncomeViewModel
    {
        private HomeBudgetDBEntities1 context;

        private Income newIncome = new Income();

        public IncomeViewModel()
        {
        }

        public IncomeViewModel(HomeBudgetDBEntities1 context, Income model, int selectedPeriodID)
        {
            this.context = context;
            this.newIncome = model;
            this.SelectedPeriodID = selectedPeriodID;
        }
        
        public List<Income> Incomes
        {
            get
            {
                if (this.context != null)
                {
                    return this.context?.Incomes?.Where(i => i.PeriodID == this.SelectedPeriodID)?.ToList();
                }

                return new List<Income>();
            }
        }

        public int SelectedPeriodID
        {
            get; private set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IncomeDate
        {
            get;
            set;
        }
        
        public List<Period> Periods
        {
            get
            {
                if (this.context != null)
                {
                    if (this.context.Periods != null)
                    {
                        if (this.context.Periods.ToList().Count > 0)
                        {
                            return this.context.Periods.ToList();
                        }
                    }
                }

                return new List<Period>();
            }
        }

        public Income NewIncome
        {
            get
            {
                return newIncome;
            }

            set
            {
                newIncome = value;
            }
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