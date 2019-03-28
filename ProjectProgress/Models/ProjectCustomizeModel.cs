using DataAccessLayer;
using ProjectProgress.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjectProgress.Models
{
    public class ProjectCustomizeModel : Project
    {
        #region Methode
        public List<Project> GetAll()
        {
            var projects = new ProgressProjectsEntities().Projects.ToList();
            return projects;
        }

        public string AddProject(Project pr)
        {
            string res ="";
            var db = new ProgressProjectsEntities();
            try
            {
                db.Projects.Add(pr);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Invalid data : " + ex.Message;
            }
            return res;
        }

        //public List<T> GetAll2<T>()
        //{
        //    try
        //    {
        //        var xx = new SqlConnexion();
        //        SqlConnection con = xx.GetConnexion();
        //        SqlCommand cmd = new SqlCommand("prGetProject", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        List<T> listReturn = new List<T>();
        //        T obj = default(T);

        //        while (reader.Read())
        //        {
        //            obj = Activator.CreateInstance<T>();

        //            foreach (PropertyInfo prop in obj.GetType().GetProperties())
        //            {
        //                if (!object.Equals(reader[prop.Name], DBNull.Value))
        //                {
        //                    prop.SetValue(obj, reader[prop.Name], null);
        //                }
        //            }
        //            listReturn.Add(obj);
        //        }
        //        return listReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("error update equipe", ex);
        //    }
        //}
#endregion
    }
}