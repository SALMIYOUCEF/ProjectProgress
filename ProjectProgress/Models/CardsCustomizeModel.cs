using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class CardsCustomizeModel
    {
        #region Methode
        public List<Cards> GetAll()
        {
            var cardList = new ProgressProjectsEntities().Cards.ToList();
            return cardList;
        }

        public string AddCard(Cards card)
        {
            string res = "";
            var db = new ProgressProjectsEntities();
            try
            {
                db.Cards.Add(card);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Invalid data : " + ex.Message; 
            }
            return res;
        }
        #endregion
    }
}