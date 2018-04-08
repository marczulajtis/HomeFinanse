using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Periods.Models
{
    public class PeriodViewModel
    {
        private HomeBudgetDBEntities context;
        private Period selectedPeriod = new Period();

        public PeriodViewModel()
        { }

        public PeriodViewModel(HomeBudgetDBEntities context)
        {
            this.context = context;
        }
        
        public Period SelectedPeriod
        {
            get { return this.selectedPeriod; }
            set { this.selectedPeriod = value; }
        }

        public List<Period> Periods
        {
            get
            {
                if (this.context != null)
                {
                    if (this.context.Periods != null)
                    {
                        return this.context.Periods.ToList();
                    }
                }

                return new List<Period>();
            }
        }
    }
}