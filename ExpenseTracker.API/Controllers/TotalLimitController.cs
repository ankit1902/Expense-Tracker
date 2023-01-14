using ExpenseTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTracker.API.Controllers
{
    public class TotalLimitController : ApiController
    {
        DBContext db = new DBContext();

        // GET: api/TotalLimit
        public IHttpActionResult Get()
        {
            List<total_limits> totalLimits = db.total_limits.ToList();
            return Ok(totalLimits);
        }

        [Route("api/getallinformation")]
        public IHttpActionResult GetAllInformation()
        {
            OverViewModel overViewData = new OverViewModel();
            overViewData.totalLimit = db.total_limits.ToList().FirstOrDefault().total_limit.ToString();
            var expenseSpent = 0;
            IEnumerable<expens> expenses = db.expenses.ToList();
            foreach(var expense in expenses)
            {
                expenseSpent = (int)(expenseSpent + expense.amount);
            }
            overViewData.expenseSpent = expenseSpent.ToString();
            overViewData.totalCategories = db.categories.ToList().Count().ToString();
            return Ok(overViewData);    
        }

        // GET: api/TotalLimit/5
        public IHttpActionResult Get(int id)
        {
            total_limits totalLimit = db.total_limits.Find(id);
            return Ok(totalLimit);
        }

        // POST: api/TotalLimit
        public IHttpActionResult Post([FromBody] total_limits totalLimit)
        {
            db.total_limits.Add(totalLimit);
            db.SaveChanges();
            return Ok(totalLimit);
        }

        // PUT: api/TotalLimit
        public IHttpActionResult Put([FromBody] total_limits totalLimit)
        {
            db.total_limits.AddOrUpdate(totalLimit);
            db.SaveChanges();
            return Ok(totalLimit);
        }

        // DELETE: api/TotalLimit/5
        public IHttpActionResult Delete(int id)
        {
            total_limits totalLimit = db.total_limits.Find(id);
            db.total_limits.Remove(totalLimit);
            IEnumerable<expens> expenses = db.expenses.ToList().Where(x => x.category == id);
            foreach(var expense in expenses)
            {
                db.expenses.Remove(expense);
                db.SaveChanges();
            }
            db.SaveChanges();
            return Ok(totalLimit);
        }
    }
}
