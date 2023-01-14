using ExpenseTracker.WEB.Models;
using ExpenseTracker.WEB.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.WEB.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Category()
        {
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                ViewBag.categories = consumeAPI.generaticReadAsAsyncs("Category");
            }
            return View();
        }

        public ActionResult Edit(long id)
        {
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                Category category = consumeAPI.generaticReadAsAsync("Category/" + id);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(long id, Category category)
        {
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                category.id = id;
                var updatedCategory = consumeAPI.generaticPutAsJsonAsync("Category", category);
            }
            return RedirectToAction("Category");
        }

        public ActionResult Delete(long id)
        {
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                var deletedCategory = consumeAPI.generaticDeleteAsync("Category/" + id);
            }
            return RedirectToAction("Category");
        }

        public ActionResult ErrorPage()
        {
            ViewBag.Message = "Total Expense Limit has been exceeded. You can't add new category";
            return View();
        }

        [HttpPost]
        public ActionResult Category(Category category)
        {
            var totalLimit = 0;
            var categoryLimit = 0;
            using (ConsumeAPI<TotalLimit> consumeAPI = new ConsumeAPI<TotalLimit>())
            {
                totalLimit = (int)consumeAPI.generaticReadAsAsyncs("TotalLimit").FirstOrDefault().total_limit;
            }
            using (ConsumeAPI<Category> consumeAPI = new ConsumeAPI<Category>())
            {
                IEnumerable<Category> categories = consumeAPI.generaticReadAsAsyncs("Category");
                foreach (var item in categories)
                {
                    categoryLimit += (int)item.expense_limit;
                }
                categoryLimit += (int)category.expense_limit;
                if (categoryLimit <= totalLimit)
                {
                    category.id = 0;
                    var insertedCategory = consumeAPI.generaticPostAsJsonAsync("Category", category);
                    return RedirectToAction("Category");
                } 
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
        }

        public ActionResult Spent()
        {
            return View();
        }
    }
}