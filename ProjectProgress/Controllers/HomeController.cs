using DataAccessLayer;
using ProjectProgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectProgress.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ProgressProjectsEntities db = new ProgressProjectsEntities();

        #region - property 
        private string _userId;

        public string UserId
        {
            get
            {
                return _userId ?? new Models.ApplicationDbContext().Users.Where(p => p.UserName == this.User.Identity.Name).FirstOrDefault().Id;
            }
            private set
            {
                _userId = value;
            }
        }
        #endregion

        /// <summary>
        /// return all project
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var projectUser = db.Projects.Where(p => p.UserId == UserId);
            var projectList = new List<CustomModelProject>();
            foreach (var pr in projectUser)
            {
                var newPr = new CustomModelProject()
                {
                    Color = pr.Color,
                    Id = pr.Id,
                    Name = pr.Name,
                    UserId = pr.UserId,
                    DteConsult = pr.DteConsult,
                    Boards = pr.Boards
                };
                projectList.Add(newPr);
            }

            var eventList = db.Events.Where(e => e.UserId == UserId);
            var taskList = new List<ProjectDetails>();
            foreach (var pr in projectUser.ToList())
            {
                foreach (var bo in pr.Boards)
                {
                    foreach (var ca in bo.Cards)
                    {
                        foreach (var task in ca.Tasks)
                        {
                            var taskAdd = new ProjectDetails();
                            taskAdd.TaskId = task.Id;
                            taskAdd.TaskName = task.Title;
                            taskAdd.ProjectColor = pr.Color;
                            taskList.Add(taskAdd);
                        }
                    }
                }
            }
            ViewBag.TaskList = taskList;
            ViewBag.EventList = eventList.ToList();
            return await System.Threading.Tasks.Task.Run(() => View(projectList));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AllProjectDetail(string searchString = null)
        {
            ViewData["CurrentFilter"] = searchString;
            var projectDetail = new List<ProjectDetails>();
            var projectUser = db.Projects.Where(p => p.UserId == UserId);
            foreach (var pr in projectUser.ToList())
            {
                foreach (var bo in pr.Boards)
                {
                    foreach (var ca in bo.Cards)
                    {
                        foreach (var task in ca.Tasks)
                        {
                            var prDetail = new ProjectDetails();
                            prDetail.ProjectName = pr.Name;
                            prDetail.ProjectColor = pr.Color;
                            prDetail.BoardName = bo.Name;
                            prDetail.CardName = ca.Title;
                            prDetail.TaskName = task.Title;
                            prDetail.TaskDescription = task.Description;
                            prDetail.TaskId = task.Id;
                            prDetail.CardId = ca.Id;
                            projectDetail.Add(prDetail);
                        }
                    }
                }
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                projectDetail = projectDetail.Where(p => p.ProjectName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(projectDetail);
        }

        /// <summary>
        /// return detail project
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        public ActionResult ProjectDetail(int id)
        {
            return RedirectToAction("GetBoards", "Board", new { idProject = id });
        }

        [HttpPost]
        /// <summary>
        /// Create new project
        /// </summary>
        /// <returns></returns>
        public JsonResult CreateProject(CustomModelProject pr)
        {
            bool hasError = true;
            pr.UserId = this.UserId;
            pr.DteConsult = DateTime.Now;

            var newProject = new Project
            {
                Color = pr.Color,
                Id = pr.Id,
                Name = pr.Name,
                UserId = pr.UserId,
                DteConsult = pr.DteConsult,
                Boards = pr.Boards
            };

            db.Projects.Add(newProject);
            db.SaveChanges();
            hasError = false;
            return Json(new {  hasError }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// je dois créer un nv controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Calender()
        {
            var events = db.Events.ToList();
            var eventList = new List<CustomModelEvent>();
            foreach (var item in events)
            {
                var newEvent = new CustomModelEvent()
                {
                    Id = item.Id,
                    Color = item.Color != null ? item.Color : item.Task.Card.Board.Project.Color,
                    Description = item.Description,
                    Title = item.Title,
                    DteEnd = item.DteEnd,
                    DteStart = item.DteStart,
                    TaskId = item.TaskId,
                };
                eventList.Add(newEvent);
            }
            return View(eventList);
        }
    }
}