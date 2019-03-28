using DataAccessLayer;
using ProjectProgress.Models;
using ProjectProgress.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectProgress.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
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
        public ActionResult Index()
        {
            var projectUser = new ProjectCustomizeModel().GetAll().Where(p => p.UserId == UserId);
            return View(projectUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AllProjectDetail()
        {
            var projectDetail = new List<ProjectDetails> ();
            var projectUser = new ProjectCustomizeModel().GetAll().Where(p => p.UserId == UserId);
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
                            projectDetail.Add(prDetail);
                        }
                    }
                }
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

        /// <summary>
        /// Create new project
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateProject()
        {
            return PartialView("_NewProject");
        }

        [HttpPost]
        /// <summary>
        /// Create new project
        /// </summary>
        /// <returns></returns>
        public JsonResult CreateProject(Project pr)
        {
            string message="Data Not Valid";
            bool hasError = true;

            pr.UserId = this.UserId;
            pr.DteConsult = DateTime.Now;
            if (!ModelState.IsValid)
            {
                var prModel = new ProjectCustomizeModel();
                message = prModel.AddProject(pr);
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
        /// <summary>
        /// je dois créer un nv controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Calender()
        {
            return View();
        }

    }
}