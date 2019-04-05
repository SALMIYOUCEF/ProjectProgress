using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace ProjectProgress.Controllers
{
    public class EventsController : Controller
    {
        private ProgressProjectsEntities db = new ProgressProjectsEntities();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events;
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.TaskId = new SelectList(db.Tasks, "Id", "Title");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TaskId,DteStart,DteEnd,Description,Title,Color")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskId = new SelectList(db.Tasks, "Id", "Title", @event.TaskId);
            return View(@event);
        }

        // GET: Events/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.TaskId = new SelectList(db.Tasks, "Id", "Title", @event.TaskId);
        //    return View(@event);
        //}

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit( Event @event)
        {
            var status = false;

            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                status = true;

            }

            return new JsonResult { Data = new { status = status } };
            
        }

        // GET: Events/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(@event);
        //}

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int? id)
        {
            var status = false; 
            Event @event = db.Events.Find(id);
            if (@event != null)
            {
                db.Events.Remove(@event);
                db.SaveChanges();
                status = true; 
            }

            return new JsonResult {Data = new {status = status} }; 
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
