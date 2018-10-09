﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Models
{
    public class UserViewModel
    {
        private HomeBudgetDBEntities1 context;

        public UserViewModel()
        { }
        
        public HomeBudgetDBEntities1 Context { get { return this.context; } set { this.context = value; } }

        public User LoginUser { get; set; }
        
        public bool RememberMe { get; set; }

        public User GetUserDataToCompare()
        {
            return this.context.Users.ToList().Find(x => x.UserName == LoginUser.UserName);
        }
    }
}