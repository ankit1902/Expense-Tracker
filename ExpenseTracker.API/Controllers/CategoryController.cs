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
    public class CategoryController : ApiController
    {
        DBContext db = new DBContext();

        // GET: api/Category
        public IHttpActionResult Get()
        {
            List<category> categories = db.categories.ToList();
            return Ok(categories);
        }

        // GET: api/Category/5
        public IHttpActionResult Get(int id)
        {
            category category = db.categories.Find(id);
            return Ok(category);
        }

        // POST: api/Category
        public IHttpActionResult Post([FromBody] category category)
        {
            db.categories.Add(category);
            db.SaveChanges();
            return Ok(category);
        }

        // PUT: api/Category/5
        public IHttpActionResult Put([FromBody] category category)
        {
            db.categories.AddOrUpdate(category);
            db.SaveChanges();
            return Ok(category);
        }

        // DELETE: api/Category/5
        public IHttpActionResult Delete(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
            IEnumerable<expens> expenses = db.expenses.ToList().Where(x => x.category == id);
            foreach (var expense in expenses)
            {
                db.expenses.Remove(expense);
                db.SaveChanges();
            }
            db.SaveChanges();
            return Ok(category);
        }
    }
}
