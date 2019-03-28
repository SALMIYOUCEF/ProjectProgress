using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class BoardCustomizeModel : Board
    {
        #region Methode
        public List<Board> GetAll()
        {
            var boardList = new ProgressProjectsEntities().Boards.ToList();
            return boardList;
        }

        public string AddBoard(Board board)
        {
            string res = "";
            var db = new ProgressProjectsEntities();
            try
            {
                 db.Boards.Add(board);
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