using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectProgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectProgress.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var map = new Dictionary<string, List<ApplicationUser>>();
            var db = new Models.ApplicationDbContext();
            var roleList = db.Roles.ToList();
            foreach(var role in roleList)
            {
                var userList = db.Users;
                var userLisByRole = userList.Where(u => u.Roles.Select(r => r.RoleId).FirstOrDefault() == role.Id ).ToList();
                map.Add(role.Name, userLisByRole);
            }
            return View(map);
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult RegisterAdmin()
        {
            return PartialView("_AdminNewUser");
        }

        public JsonResult DeleteUser(string id)
        {
            bool result = false; 
            var db = new Models.ApplicationDbContext();
            var user = db.Users.Select(u => u.Id == id.ToString());
            if (user != null)
            {
                db.Users.Remove(db.Users.Where(u => u.Id == id.ToString()).First());
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}