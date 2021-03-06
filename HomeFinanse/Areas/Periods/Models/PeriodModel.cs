﻿using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Periods.Models
{
    public class PeriodModel : Period
    {
        private Nullable<DateTime> nullableStartDate;
        private Nullable<DateTime> nullableEndDate;
        public Nullable<DateTime> NullableStartDate
        {
            get
            {
                if (PeriodStart != new DateTime())
                {
                    nullableStartDate = ((Nullable<DateTime>)PeriodStart);
                }

                return nullableStartDate;
            }
            set
            {
                nullableStartDate = value;
            }
        }
        public Nullable<DateTime> NullableEndDate
        {
            get
            {
                if (PeriodEnd != new DateTime())
                {
                    nullableEndDate = ((Nullable<DateTime>)PeriodEnd);
                }

                return nullableEndDate;
            }
            set
            {
                nullableEndDate = value;
            }
        }
    }
}