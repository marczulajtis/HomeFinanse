//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeFinanse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Income
    {
        public int ID { get; set; }
        public string IncomeName { get; set; }
        public decimal Value { get; set; }
        public int CategoryID { get; set; }
        public string Place { get; set; }
        public Nullable<System.DateTime> IncomeDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int PeriodID { get; set; }
        public bool OnAccount { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Period Period { get; set; }
    }
}
