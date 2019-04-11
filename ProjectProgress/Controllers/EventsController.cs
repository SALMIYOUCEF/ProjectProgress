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

        [HttpPost]
        public JsonResult Create(Event @event)
        {
            var status = false;

            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }


        // POST: Events/Edit/5
        [HttpPost]
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
