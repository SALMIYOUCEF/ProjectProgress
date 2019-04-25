using DataAccessLayer;
using ProjectProgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectProgress.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private ProgressProjectsEntities db = new ProgressProjectsEntities();
        /// <summary>
        /// return all boards with the Id project equal idProject in paramater
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        // GET: Borad
        public ActionResult GetBoards(int idProject)
        {
            var project = db.Projects.ToList().First(p => p.Id == idProject);
            ViewBag.Project = project;
            var boards = db.Boards.Where(b => b.ProjectId == idProject).ToList();

            var boardList = new List<CustomModelBoards> ();
            foreach (var board in boards)
            {
                var newBoard = new CustomModelBoards()
                {
                    Cards = board.Cards,
                    Name = board.Name,
                    Id = board.Id,
                    Project = board.Project,
                    ProjectId = board.ProjectId
                };
                boardList.Add(newBoard);

            }
           
            return View(boardList);
        }

        [HttpGet]
        public ActionResult AddBoard()
        {
            //return ("test return new board");
            return PartialView("_NewBoard");
        }

        [HttpPost]
        public JsonResult AddBoard(CustomModelBoards board)
        {
            string message = "Data Not Valid";
            bool hasError = true;
            
            if (ModelState.IsValid)
            {
                var newBoard = new Board()
                {
                    Cards = board.Cards,
                    Name = board.Name,
                    Id = board.Id,
                    Project = board.Project,
                    ProjectId = board.ProjectId
                };
                db.Boards.Add(newBoard);
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
    }
}