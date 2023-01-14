using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.WEB.Models
{
    public class Category
    {
        public long id { get; set; }
        public string name { get; set; }
        public Nullable<long> expense_limit { get; set; }
    }
}