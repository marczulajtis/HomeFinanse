using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Periods.Models
{
    public class PeriodModel : Period
    {
        private Nullable<DateTime> nullableStartDate = DateTime.Now;
        private Nullable<DateTime> nullableEndDate = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
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