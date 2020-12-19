using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Employee
        public IQueryable<Usertbl> GetEmployees()
        {
            return db.Usertbls;
        }


        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Usertbl employee)
        {

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employee
        [ResponseType(typeof(Usertbl))]
        public IHttpActionResult PostEmployee(Usertbl employee)
        {
            db.Usertbls.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(Usertbl))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Usertbl employee = db.Usertbls.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Usertbls.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Usertbls.Count(e => e.EmployeeID == id) > 0;
        }
    }
}  