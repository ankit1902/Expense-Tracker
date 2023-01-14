using ExpenseTracker.API.Models;
using ExpenseTracker.WEB.Models;
using ExpenseTracker.WEB.Wrapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ConsumeAPI<OverViewModel> consumeAPI = new ConsumeAPI<OverViewModel>())
            {
                OverViewModel overViewData = consumeAPI.generaticReadAsAsync("getallinformation");
                ViewBag.totalLimit = overViewData.totalLimit;
                ViewBag.expenseSpent = overViewData.expenseSpent;
                ViewBag.totalCategories = overViewData.totalCategories;
            }
            using(ConsumeAPI<Expense> consumeAPI =  new ConsumeAPI<Expense>())
            {
                ViewBag.expenseList = consumeAPI.generaticReadAsAsyncs("Expense");
            }
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                ViewBag.categories = consumeAPI.generaticReadAsAsyncs("Category");
            }
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        public ActionResult DashBoard()
        {
            ViewBag.Message = "Your DashBoard page.";

            return View();
        }
    }
}