using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Controllers
{
    public class TrackingController : BaseController
    {
        private Models.TrackingModel.TrackingState state;
        Models.ApplicationDbContext db = new Models.ApplicationDbContext();

        // GET: Tracking
        public ActionResult Index()
        {
            // objects.Add(new Models.TrackingModel.TrackingState(2, "3:00", "Ballyfermot", 30.00, 4));
            return View();
        }

        public ActionResult Sessions() {
            return View();
        }

        public ActionResult Session(int id)
        {
            //var session = db.Sessions.Include(x => x.states).Where(x => x.Id == id);
            //var session = db.Sessions.Find(id);
            Models.TrackingModel.Session session = db.Sessions.Include(s => s.Session_Id).Where(p => p.Id == id ).First();

            return View(session);
        }


        public string Welcome() {
            return "This is the welcome action method.";
        }
    }
}