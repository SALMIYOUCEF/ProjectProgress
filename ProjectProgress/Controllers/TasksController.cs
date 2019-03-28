using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using ProjectProgress.Models;

namespace ProjectProgress.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        [HttpGet]
        public ActionResult AddTask(int idCard)
        {
            var boardList = new ProgressProjectsEntities().Cards.ToList();
            var idBoard = boardList.Where(c => c.Id == idCard).First().IdBoard;
            ViewBag.IdBoard = idBoard; 
            ViewBag.Id = idCard;

            return PartialView("_NewTask");
        }
        
        [HttpPost]
        public JsonResult AddTask(Task task)
        {
            string message = "Data Not Valid";
            bool hasError = true;
            if (ModelState.IsValid)
            {
                TasksCustomizeModel taskModel = new TasksCustomizeModel();
                message = taskModel.AddTask(task);

                if (message == "")
                {
                    hasError = false;
                }
                return Json(new { message, hasError }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message, hasError }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}