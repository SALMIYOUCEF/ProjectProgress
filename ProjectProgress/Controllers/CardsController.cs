using DataAccessLayer;
using ProjectProgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ProjectProgress.Controllers
{
    [Authorize]
    public class CardsController : Controller
    {
        private ProgressProjectsEntities db = new ProgressProjectsEntities();

        // GET: Cards
        public ActionResult GetCards(int boardId)
        {
            var cardList = new List<CustomModelCard>();
            var board = db.Boards.FirstOrDefault(b => b.Id == boardId);
            ViewBag.Board = board;
            var cards = db.Cards.Where(c => c.IdBoard == boardId).ToList();

            foreach (var card in cards)
            {
                var newCard = new CustomModelCard()
                {
                    Title = card.Title,
                    Id = card.Id,
                    Board = card.Board,
                    Tasks = card.Tasks,
                    IdBoard = card.IdBoard
                };
                cardList.Add(newCard);
            }
            return PartialView("_GetCards", cardList);
        }

        [HttpGet]
        public ActionResult CreateCard(int BoardId)
        {
            ViewBag.Id = BoardId;
            return PartialView("_NewCard");
        }


        [HttpPost]
        public JsonResult CreateCard(CustomModelCard card)
        {
            string message = "Data Not Valid";
            bool hasError = true;
            if (ModelState.IsValid)
            {
                var newCard = new Cards()
                {
                    Title = card.Title,
                    Id = card.Id,
                    Board = card.Board,
                    Tasks = card.Tasks,
                    IdBoard = card.IdBoard
                };
                db.Cards.Add(newCard);
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