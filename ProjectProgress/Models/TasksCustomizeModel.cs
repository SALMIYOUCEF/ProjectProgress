using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProgress.Models
{
    public class TasksCustomizeModel
    {
#region Methode
        public string AddTask(Task task)
        {
            string res = "";
            var db = new ProgressProjectsEntities();
            try
            {
                db.Tasks.Add(task);
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