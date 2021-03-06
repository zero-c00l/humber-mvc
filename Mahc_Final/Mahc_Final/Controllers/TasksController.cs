﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mahc_Final.DBContext;
using PagedList;
using Mahc_Final.ViewModels;
using Mahc_Final.Helpers;

namespace Mahc_Final.Controllers
{
    public class TasksController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Tasks
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.StatusSortParm = sortOrder == "Published" ? "not_published" : "Published";
            ViewBag.CreatedSortParm = sortOrder == "Created" ? "created_desc" : "Created";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tasks = from s in db.Tasks
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.Title.Contains(searchString)
                                       || s.Desc.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    tasks = tasks.OrderByDescending(s => s.Title);
                    break;
                case "Published":
                    tasks = tasks.OrderByDescending(s => s.Status);
                    break;
                case "not_published":
                    tasks = tasks.OrderBy(s => s.Status);
                    break;
                case "Created":
                    tasks = tasks.OrderBy(s => s.Created_date);
                    break;
                case "created_desc":
                    tasks = tasks.OrderByDescending(s => s.Created_date);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(s => s.Modified_date);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(s => s.Modified_date);
                    break;
                default:
                    tasks = tasks.OrderBy(s => s.Title);
                    break;
            }
            //var tasks = db.Tasks.Include(j => j.Job_types);
            foreach (Task a in tasks)
            {
                a.Desc = Helpers.HtmlDescriptionHelper.GetShortDescFromHtml(a.Desc);
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View("Admin/Index", tasks.ToPagedList(pageNumber, pageSize));
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View("Admin/Details", task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View("Admin/Create");
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Type,Time,Regularity,Desc,Status,Contact_person,Contact_phone")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.Created_by = 1;
                task.Modified_by = 1;
                task.Created_date = DateTime.Now;
                task.Modified_date = DateTime.Now;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Admin/Create", task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View("Admin/Edit", task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Type,Time,Regularity,Desc,Status,Contact_person,Contact_phone")] Task task)
        {
            Task currentTask = db.Tasks.FirstOrDefault(j => j.Id == task.Id);
            if (ModelState.IsValid)
            {
                //db.Entry(task).State = EntityState.Modified;
                currentTask.Title = task.Title;
                currentTask.Type = task.Type;
                currentTask.Time = task.Time;
                currentTask.Regularity = task.Regularity;
                currentTask.Desc = task.Desc;
                currentTask.Status = task.Status;
                currentTask.Contact_person = task.Contact_person;
                currentTask.Contact_phone = task.Contact_phone;
                currentTask.Modified_by = 2;
                currentTask.Modified_date = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Admin/Edit", task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);

            if (task == null)
            {
                return HttpNotFound();
            }
            return View("Admin/Delete", task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            try
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            catch (Exception dex)
            {
                ViewBag.Message = "Something went wrong...";
                Volunteer vol = db.Volunteers.Where(a => a.Task_id== id).First();
                if (vol != null)
                {
                    ViewBag.Message += " You were trying to delete task which have active volunteers";
                }
                return View("Admin/Delete", task);
            }
           
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
        //Public
        public ActionResult PublicIndex()
        {
            var tasks= db.Tasks.OrderBy(s => s.Modified_date).Where(j => j.Status == true);
            return View("Public/Index", tasks.ToList());
        }

        public ActionResult PublicDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                //a request will be sent to the database here. 
                Task tasks = db.Tasks.Find(id);
                if (tasks == null)
                {
                    return HttpNotFound();
                }
                TaskVolunteer tskvol = new TaskVolunteer();
                Volunteer apn = new Volunteer();
                apn.Task_id = (int)id;
                tskvol.task = tasks;
                tskvol.application = apn;
                //ViewBag.Task_id = db.Tasks.Where(j => j.Id==id && j.Status == true);
                //ViewBag.Task_id = db.Tasks.Where(j =>j.Status == true);
                return View("Public/Details", tskvol);
            }
            catch (Exception dex) //this catch is finding a server error. 
            {
                ViewBag.Message = "Something went wrong: " + dex.Message;
            }
            return RedirectToAction("PublicIndex"); //if the try was successful, then the return above would execute.
                                                    //this return would execute if catch was needed
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Job_id,Name,Email,Phone,CV,Text")]
        public ActionResult PublicDetails(TaskVolunteer tskvol)
        {
            try //if you auto build the controllers, visual studio will NOT include a try/catch
            { //a try/catch will try what you want to do, then "catch" what goes wrong. Try/catch can even catch server errors such as if the database server is down
                if (ModelState.IsValid)
                {

                    tskvol.application.Date = DateTime.Now;
                    db.Volunteers.Add(tskvol.application);
                    db.SaveChanges();
                    return RedirectToAction("PublicIndex");
                }
            }
            catch (DataException dex) //you can create an Exception/DataException object here and set it to a variable. I've called it dex here. 
            {
                ViewBag.Message = "Whoops! Something went wrong. Here's what went wrong: " + dex.Message; //One of the properties of these objects is Message which is a string of what went wrong. 
            }


            tskvol.task = db.Tasks.Find(tskvol.application.Task_id);
            // ViewBag.Type = new SelectList(db.Job_types, "Id", "Title", jobs.Type);
            return View("Public/Details", tskvol);
        }
    }
}
