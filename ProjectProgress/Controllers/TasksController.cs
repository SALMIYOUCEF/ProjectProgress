using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using ProjectProgress.Models;
using System.Data.Entity;

namespace ProjectProgress.Controllers
{
    public class TasksController : Controller
    {
        private ProgressProjectsEntities db = new ProgressProjectsEntities();

        // GET: Tasks
        [HttpGet]
        public ActionResult AddTask(int idCard)
        {
            var idBoard = db.Cards.Where(c => c.Id == idCard).First().IdBoard;
            ViewBag.IdBoard = idBoard;
            ViewBag.Id = idCard;

            return PartialView("_NewTask");
        }

        [HttpPost]
        public JsonResult AddTask(CustomModelTask task)
        {
            string message = "Data Not Valid";
            bool hasError = true;
            if (ModelState.IsValid)
            {
                var newTask = new Task()
                {
                    Event = task.Event,
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Card = task.Card,
                    IdCard = task.IdCard
                };
                db.Tasks.Add(newTask);
                db.SaveChanges();
                message = "";
                hasError = false;
                return Json(new { message, hasError }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message, hasError }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Edit(Task task)
        {
            var status = false;

            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }
        public JsonResult Delete(int? id)
        {
            var status = false;
            Task task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Events.RemoveRange(db.Events.Where(e=>e.TaskId == id));
                db.Tasks.Remove(task);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}