using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using System.Linq;
using System.Web.Helpers;
using ProjectProgress.Models;
using System.Threading.Tasks;

namespace ProjectProgress.Controllers
{
    public class EventsController : Controller
    {
        private ProgressProjectsEntities db = new ProgressProjectsEntities();

        // GET: Events
        public ActionResult Index(string userId)
        {
            var events = db.Events.Where(e => e.UserId == userId);
            var customEvent = new List<CustomModelEvent>();
            foreach (var e in events)
            {
                var newEvent = new CustomModelEvent()
                {
                    Color = e.Color,
                    Title = e.Title,
                    DteStart = e.DteStart,
                    DteEnd = e.DteEnd,
                    TaskId = e.TaskId,
                    Description = e.Description,
                    Id = e.Id,
                    UserId = e.UserId,
                    Task = e.Task
                };
                customEvent.Add(newEvent);
            }
            return  PartialView(customEvent);
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
        public async Task<JsonResult> Create(Event @event)
        {
            var hasError = true;

            if (ModelState.IsValid)
            {
                if (@event.Color == null && @event.TaskId != 0)
                {
                    var projectEvent = db.Tasks.Where(p => p.Id == @event.TaskId).Select(t => t.Card.Board.Project).First();
                    @event.Color = projectEvent.Color;
                    @event.UserId = projectEvent.UserId;
                    // @event
                }

                var newEvent = new Event()
                {
                    Color = @event.Color,
                    Title = @event.Title,
                    DteStart = @event.DteStart,
                    TaskId = @event.TaskId,
                    Description = @event.Description,
                    DteEnd = @event.DteEnd,
                    UserId = @event.UserId,
                    Id = @event.Id,
                    Task = @event.Task
                };
                db.Events.Add(newEvent);
                await db.SaveChangesAsync();
                hasError = false;
            }

            return Json ( new { hasError = hasError }, JsonRequestBehavior.AllowGet );
        }


        // POST: Events/Edit/5
        [HttpPost]
        public JsonResult Edit(CustomModelEvent @event)
        {
            var hasError = false;
            if (ModelState.IsValid)
            {
                var newEditedEvent = new Event()
                {
                    Color = @event.Color,
                    Title = @event.Title,
                    DteStart = @event.DteStart,
                    TaskId = @event.TaskId,
                    Description = @event.Description,
                    DteEnd = @event.DteEnd,
                    UserId = @event.UserId,
                    Id = @event.Id,
                    Task = @event.Task
                };
                db.Entry(newEditedEvent).State = EntityState.Modified;
                db.SaveChanges();
                hasError = true;
            }
            return Json ( new{ hasError = hasError } );
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

            return new JsonResult { Data = new { status = status } };
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
