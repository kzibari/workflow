using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Workflow.Models;

namespace Workflow.Controllers
{
    public class AssignmentsController : Controller
    {
        private Workflow2Entities db = new Workflow2Entities();

        // GET: Assignments
        public ActionResult Index(DateTime? byDate, string searchTerm = null, string status = null)
        {
            var assignments = db.Assignments.Include(a => a.Employee).Include(a => a.Task);
            

            //Check for search criteria: date, name/email, completed/outstanding

           if(status=="Completed")
                assignments = db.Assignments.Include(a => a.Employee).Include(a => a.Task).Where(item=>status==null|| item.Completed==true);
           if ((status == "Outstanding"))
                assignments = db.Assignments.Include(a => a.Employee).Include(a => a.Task).Where(item => status == null || item.Completed == false);

            if (byDate != null)
            {
                //store total assignments in ViewBag
                ViewBag.Total = assignments.ToList().OrderBy(item => item.DueDate).Where(item => byDate == null || item.DueDate == byDate).Count();
                return View(assignments.ToList().OrderBy(item => item.DueDate).Where(item => byDate == null || item.DueDate == byDate));
            }
            else
            {
                //store total assignments in ViewBag
                ViewBag.Total = assignments.ToList().OrderBy(item => item.DueDate).Where(item => searchTerm == null || item.Employee.Name.Contains(searchTerm) ||
                                                                                                                     item.Employee.Email.Contains(searchTerm)).Count();

                return View(assignments.ToList().OrderBy(item => item.DueDate).Where(item => searchTerm == null || item.Employee.Name.Contains(searchTerm) ||
                                                                                                                   item.Employee.Email.Contains(searchTerm)));
            }
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Title");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignmentID,SetDate,DueDate,Completed,EmployeeID,TaskID,CompletionDate")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", assignment.EmployeeID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Title", assignment.TaskID);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", assignment.EmployeeID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Title", assignment.TaskID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignmentID,SetDate,DueDate,Completed,EmployeeID,TaskID,CompletionDate")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", assignment.EmployeeID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "Title", assignment.TaskID);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
