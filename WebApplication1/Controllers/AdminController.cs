using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            try
            {
                var db = new Models.ApplicationDbContext();
                var users = db.Users.ToList();
                Success("All users retrieved", true);
                return View(users);
            }
            catch (Exception ex) {
                Warning(ex.Message);
                return View(ex.Message);
            }
         
        }
    }
}