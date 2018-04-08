using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Outcomes.Models
{
    public class OutcomeNotNullable : Outcome
    {
        bool plannedNotNullable = false;

        public bool PlannedNotNullable
        {
            get
            {
                return this.plannedNotNullable;
            }

            set
            {
                this.plannedNotNullable = value;
            }
        }

        public string MonthName
        {
            get
            {
                return ((DateTime)this.Date).ToString("MMMM");
            }
        }
    }
}