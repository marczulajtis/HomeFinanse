using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Incomes.Models
{
    public class IncomeViewModel
    {
        private HomeBudgetDBEntities context;

        private Income newIncome = new Income();

        public IncomeViewModel()
        {
        }

        public IncomeViewModel(HomeBudgetDBEntities context, Income model)
        {
            this.context = context;
            this.newIncome = model;
        }

        public List<Income> Incomes
        {
            get
            {
                if (this.context != null)
                {
                    return this.context.Incomes.ToList();
                }

                return new List<Income>();
            }
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
    }
}