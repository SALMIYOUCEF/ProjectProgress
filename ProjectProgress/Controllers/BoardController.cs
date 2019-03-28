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
        /// <summary>
        /// return all boards with the Id project equal idProject in paramater
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        // GET: Borad
        public ActionResult GetBoards(int idProject)
        {
            var project = new ProgressProjectsEntities().Projects.ToList().First(p => p.Id == idProject);
            ViewBag.Project = project;
            var boardList = new BoardCustomizeModel().GetAll().Where(b => b.ProjectId == idProject).ToList();

            return View(boardList);
        }

        [HttpGet]
        public ActionResult AddBoard()
        {
            //return ("test return new board");
            return PartialView("_NewBoard");
        }

        [HttpPost]
        public JsonResult AddBoard(Board board)
        {
            string message = "Data Not Valid";
            bool hasError = true;
            
            if (ModelState.IsValid)
            {
                BoardCustomizeModel boardModel = new BoardCustomizeModel();
                message = boardModel.AddBoard(board);

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