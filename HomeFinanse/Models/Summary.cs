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
    
    public partial class Summary
    {
        public int SummaryID { get; set; }
        public string SelectedPeriodID { get; set; }
    
        public virtual Summary Summary1 { get; set; }
        public virtual Summary Summary2 { get; set; }
    }
}
