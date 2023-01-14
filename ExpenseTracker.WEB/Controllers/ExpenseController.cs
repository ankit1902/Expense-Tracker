using ExpenseTracker.WEB.Models;
using ExpenseTracker.WEB.Wrapper;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.WEB.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Expense()
        {
            ViewBag.Message = "Hello";
            using (ConsumeAPI<Expense> consumeAPI = new ConsumeAPI<Expense>())
            {
                ViewBag.expenseList = consumeAPI.generaticReadAsAsyncs("Expense");
            }
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                ViewBag.categories = consumeAPI.generaticReadAsAsyncs("Category");
            }
            return View();
        }

        public ActionResult Edit(long id)
        {
            ViewBag.Message = "";
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                ViewBag.categories = consumeAPI.generaticReadAsAsyncs("Category");
            }
            using (ConsumeAPI<Expense> consumeAPI = new ConsumeAPI<Expense>())
            {
                Expense expense = consumeAPI.generaticReadAsAsync("Expense/" + id);
                return View(expense);
            }
        }

        [HttpPost]
        public ActionResult Edit(long id, Expense expense)
        {
            var expenseLimit = 0;
            var categoryLimit = 0;
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                categoryLimit = (int)consumeAPI.generaticReadAsAsync("Category/" + expense.category).expense_limit;
            }
            using (ConsumeAPI<Expense> consumeAPI = new ConsumeAPI<Expense>())
            {
                IEnumerable<Expense> expenses = consumeAPI.generaticReadAsAsyncs("Expense").Where(x => x.category == expense.category);
                foreach (var item in expenses)
                {
                    expenseLimit = (int)(expenseLimit + item.amount);
                }
                if (expenseLimit <= categoryLimit)
                {
                    expense.id = id;
                    var updatedExpense = consumeAPI.generaticPutAsJsonAsync("Expense", expense);
                    return RedirectToAction("Expense");
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
        }

        public ActionResult ErrorPage()
        {
            ViewBag.Message = "Category Expense Limit has been exceeded. You can't add new expense under this category";
            return View();
        }

        public ActionResult Delete(long id)
        {
            using (ConsumeAPI<Expense> consumeAPI = new ConsumeAPI<Expense>())
            {
                var deletedExpense = consumeAPI.generaticDeleteAsync("Expense/" + id);
            }
            return RedirectToAction("Expense");
        }

        [HttpPost]
        public ActionResult Expense(Expense expense)
        {
            var expenseLimit = 0;
            var categoryLimit = 0;
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                categoryLimit = (int)consumeAPI.generaticReadAsAsync("Category/" + expense.category).expense_limit;
            }
            using (ConsumeAPI<Expense> consumeAPI = new ConsumeAPI<Expense>())
            {
                IEnumerable<Expense> expenses = consumeAPI.generaticReadAsAsyncs("Expense").Where(x => x.category == expense.category);
                foreach (var item in expenses)
                {
                    expenseLimit = (int)(expenseLimit + item.amount);
                }
                expenseLimit += (int)expense.amount;
                if (expenseLimit <= categoryLimit)
                {
                    var insertedExpense = consumeAPI.generaticPostAsJsonAsync("Expense", expense);
                    return RedirectToAction("Expense");
                }
                else
                {
                    
                    return RedirectToAction("ErrorPage");
                }
            }
        }

        public ActionResult TotalExpense()
        {
            return View();
        }
    }
}