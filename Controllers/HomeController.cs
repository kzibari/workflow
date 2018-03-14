using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Models;

namespace Workflow.Controllers
{
    public class HomeController : Controller
    {
        private Workflow2Entities _db = new Workflow2Entities();

        // GET: Home
        [HttpGet]
        public ActionResult Index(string email)
        {
            if (email!=null)
            {
                var model = from r in _db.Assignments.ToList()
                            where (r.Completed == false && r.Employee.Email == email)
                            select r;

                return View("ViewOutstandingTasks", model);
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Complete(int id, string email)
        {
            _db.Assignments.Find(id).Completed = true;
            _db.Assignments.Find(id).CompletionDate = DateTime.Today;
            _db.SaveChanges();
            
            return RedirectToAction("Index", new { email = email });
            
        }
        [HttpPost]
        public ActionResult Refer(int id, string email)
        {
            var var = from r in _db.Employees.ToList()
                        where (r.Email != email)
                        select r;
            SelectList list = new SelectList(var, "EmployeeID", "Name");
            ViewBag.Employees = list;
            ViewBag.AssignmentID = Request["id"];
            ViewBag.Referer = email;
            return View("Refer");

        }
        [HttpPost]
        public ActionResult Assign(int id)
        {

            _db.Assignments.Find(id).EmployeeID = Int32.Parse(Request.Form["Employees"]);
            _db.SaveChanges();

            return RedirectToAction("Index", new { email = Request.Form["email"] });

        }





    }




}
