using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.WEB.Models
{
    public class Expense
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<long> amount { get; set; }
        public Nullable<long> category { get; set; }
    }
}