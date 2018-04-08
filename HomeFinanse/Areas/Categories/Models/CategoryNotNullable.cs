using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Areas.Categories.Models
{
    public class CategoryNotNullable : Category
    {
        private bool nflagNotNullable = false;

        public CategoryNotNullable()
        { }

        public bool NFLAGNotNullable
        {
            get
            {
                return nflagNotNullable;
            }

            set
            {
                nflagNotNullable = value;
            }
        }
    }
}