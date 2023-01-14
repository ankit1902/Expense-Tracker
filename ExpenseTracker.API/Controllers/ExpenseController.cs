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
    public class ExpenseController : ApiController
    {
        DBContext db = new DBContext();

        // GET: api/Expense
        public IHttpActionResult Get()
        {
            List<expens> expenses = db.expenses.ToList();
            return Ok(expenses);
        }

        // GET: api/Expense/5
        public IHttpActionResult Get(int id)
        {
            expens expense = db.expenses.Find(id);
            return Ok(expense);
        }

        // POST: api/Expense
        public IHttpActionResult Post([FromBody] expens expense)
        {
            db.expenses.Add(expense);
            db.SaveChanges();
            return Ok(expense);
        }

        // PUT: api/Expense
        public IHttpActionResult Put([FromBody] expens expense)
        {
            db.expenses.AddOrUpdate(expense);
            db.SaveChanges();
            return Ok(expense);
        }

        // DELETE: api/Expense/5
        public IHttpActionResult Delete(int id)
        {
            expens expense = db.expenses.Find(id);
            db.expenses.Remove(expense);    
            db.SaveChanges();
            return Ok(expense);
        }
    }
}
