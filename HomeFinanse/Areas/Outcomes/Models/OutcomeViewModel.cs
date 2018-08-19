using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Outcomes.Models
{
    public class OutcomeViewModel
    {
        private HomeBudgetDBEntities context;
        private OutcomeNotNullable newOutcome = new OutcomeNotNullable();

        public OutcomeViewModel()
        { }

        public OutcomeViewModel(HomeBudgetDBEntities context, OutcomeNotNullable newOutcome)
        {
            this.context = context;
            this.newOutcome = newOutcome;
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
                    return this.context.Outcomes.ToList();
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
    }
}