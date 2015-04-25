using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class TrackingController : BaseController
    {
        private static WebClient proxy = new WebClient();
        private List<Models.TrackingModel.TrackingState> objects = new List<Models.TrackingModel.TrackingState>();
        private Models.TrackingModel.TrackingState state;

        // GET: Tracking
        public ActionResult Index()
        {
            return RedirectToAction("Sessions");
        }

        public ActionResult Sessions()
        {
            return View();
        }

        public ActionResult Session(int id)
        {
            Models.TrackingModel.Session session;
            using (var db = new Models.ApplicationDbContext())
            {
                session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
            }
            return View(session);
        }

        public ActionResult GetDataForPlot(int id)
        {
            Models.TrackingModel.Session session;
            using (var db = new Models.ApplicationDbContext())
            {
                session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
            }
            var data = new List<Models.TrackingModel.TrackingTempLineChartModel>();
            //    new Models.TrackingModel.TrackingTempLineChartModel { date ="1", temp = "30"}
            //};
            foreach (var state in session.states)
            {
                if (state.time != null) {
                    data.Add(new Models.TrackingModel.TrackingTempLineChartModel(state.time, state.temp.ToString()));
                }
                
            }
            JsonResult jr = new JsonResult();
            jr.Data = data;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNoOfStatesInSession(int id) {
            Models.TrackingModel.Session session;
            using (var db = new Models.ApplicationDbContext())
            {
                session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
            }
            int length = session.states.Count;
            //    new Models.TrackingModel.TrackingTempLineChartModel { date ="1", temp = "30"}
            //};
            JsonResult jr = new JsonResult();
            jr.Data = length;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        public ActionResult GetAvgTempFromSession(int id)
        {
            double averageTemp = 0;
            Models.TrackingModel.Session session;
            using (var db = new Models.ApplicationDbContext())
            {
                session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
            }
            foreach (var state in session.states) {
                averageTemp += state.temp;
            }

            averageTemp /= session.states.Count;
            //    new Models.TrackingModel.TrackingTempLineChartModel { date ="1", temp = "30"}
            //};

            // Convert to a percentage of 30 deg
            averageTemp = (averageTemp / 30) * 100;
            averageTemp = Math.Round(averageTemp);

            JsonResult jr = new JsonResult();
            jr.Data = averageTemp;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        public ActionResult ListOfStatesID()
        {
            try
            {
                Warning("IN TRACKING FUNC");
                objects = GetAllTrackingStateByID(User.Identity.GetUserId());
                Success(string.Format("List successfully retrieved at: <strong>{0}</strong>", DateTime.Now.ToString()), true);
            }
            catch (Exception ex)
            {
                Danger(string.Format("<strong>ERROR: </strong>{0}", ex.Message), true);
            }
            // objects.Add(new Models.TrackingModel.TrackingState(2, "3:00", "Ballyfermot", 30.00, 4));
            return View(objects);
        }


        public ActionResult TrackingInfoID()
        {
            //state = GetTrackingState(Convert.ToInt32(User.Identity.GetUserId()));

            state = GetTrackingState(2147483647);
            return View(state);
        }

        public List<Models.TrackingModel.TrackingState> GetAllTrackingState()
        {
            byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/GetAllTrackingState")));

            Stream strm = new MemoryStream(toByte);
            DataContractSerializer obj = new DataContractSerializer(typeof(List<Models.TrackingModel.TrackingState>));
            var objects = (List<Models.TrackingModel.TrackingState>)obj.ReadObject(strm);
            return objects;
        }

        public Models.TrackingModel.TrackingState GetTrackingState(int id)
        {
            byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/TrackingState/latest/" + id)));

            Stream strm = new MemoryStream(toByte);
            DataContractSerializer obj = new DataContractSerializer(typeof(Models.TrackingModel.TrackingState));
            var state = (Models.TrackingModel.TrackingState)obj.ReadObject(strm);
            return state;
        }
        public List<Models.TrackingModel.TrackingState> GetAllTrackingStateByID(string id)
        {
            string user_id = id;
            //a99f2883-8ed3-4583-afdb-3f570f159cf3
            byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/TrackingState/" + id)));

            Stream strm = new MemoryStream(toByte);
            DataContractSerializer obj = new DataContractSerializer(typeof(List<Models.TrackingModel.TrackingState>));
            var objects = (List<Models.TrackingModel.TrackingState>)obj.ReadObject(strm);
            return objects;
        }

        public string Welcome()
        {
            return "This is the welcome action method.";
        }
    }
}