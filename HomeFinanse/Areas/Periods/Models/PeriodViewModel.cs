using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Periods.Models
{
    public class PeriodViewModel
    {
        private HomeBudgetDBEntities1 context;
        private Period selectedPeriod = new Period();
        private PeriodModel newPeriod;

        public PeriodViewModel()
        { }

        public PeriodViewModel(HomeBudgetDBEntities1 context, PeriodModel newPeriod)
        {
            this.context = context;
            this.newPeriod = newPeriod;
        }

        public PeriodModel NewPeriod
        {
            get
            {
                return this.newPeriod;
            }

            set
            {
                this.newPeriod = value;
            }
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