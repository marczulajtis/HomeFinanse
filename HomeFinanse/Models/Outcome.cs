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
    
    public partial class Outcome
    {
        public int ID { get; set; }
        public string OutcomeName { get; set; }
        public decimal Value { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int CategoryID { get; set; }
        public string Place { get; set; }
        public bool Planned { get; set; }
        public int PeriodID { get; set; }
        public bool Payed { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Period Period { get; set; }
    }
}
