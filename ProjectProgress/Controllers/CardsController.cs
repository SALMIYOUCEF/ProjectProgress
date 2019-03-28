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
    public class CardsController : Controller
    {
        // GET: Cards
        public ActionResult GetCards(int boardId)
        {
            var board = new BoardCustomizeModel().GetAll().FirstOrDefault(b => b.Id == boardId);
            ViewBag.Board = board;
            var cardList = new ProgressProjectsEntities().Cards.Where(c => c.IdBoard == boardId).ToList();
            return PartialView("_GetCards", cardList);
        }

        [HttpGet]
        public ActionResult CreateCard(int BoardId)
        {
            ViewBag.Id = BoardId;
            return PartialView("_NewCard");
        }


        [HttpPost]
        public JsonResult CreateCard(Cards card)
        {
            string message = "Data Not Valid";
            bool hasError = true;
            if (ModelState.IsValid)
            {
                CardsCustomizeModel boardModel = new CardsCustomizeModel();
                message = boardModel.AddCard(card);

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