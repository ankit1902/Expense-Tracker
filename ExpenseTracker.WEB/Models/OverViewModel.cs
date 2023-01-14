using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.API.Models
{
    public class OverViewModel
    {
        public string totalLimit { get; set; }
        public string expenseSpent { get; set; }
        public string totalCategories { get; set; }
    }
}