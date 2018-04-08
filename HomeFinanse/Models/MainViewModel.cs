using HomeFinanse.Areas.Periods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Models
{
    public class MainViewModel
    {
        private HomeBudgetDBEntities context;

        public MainViewModel(HomeBudgetDBEntities context)
        {
            this.context = context;
        }

        public Period SelectedPeriod
        {
            get;
            set;
        }
    }
}