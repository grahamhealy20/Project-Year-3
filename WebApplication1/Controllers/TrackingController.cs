﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;

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
            List<Models.TrackingModel.Session> sessions = GetSessions(User.Identity.GetUserId());

            return View(sessions);
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
        public Models.TrackingModel.Session GetSession(string id)
        {
            int sessionId = Convert.ToInt32(id);
           // Models.TrackingModel.Session session;
            string session_id = id;
            byte[] toByte;
            //a99f2883-8ed3-4583-afdb-3f570f159cf3
            using (WebClient client = new WebClient()) {
                toByte = client.DownloadData((new Uri("https://localhost:44301/TrackingService.svc/Session/Selected/" + sessionId)));
            }
           
            Stream strm = new MemoryStream(toByte);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(Models.TrackingModel.Session));
            var objects = (Models.TrackingModel.Session)obj.ReadObject(strm);
            //session = (Models.TrackingModel.Session) objects;
            return objects;
        }

        public List<Models.TrackingModel.Session> GetSessions(string id)
        {
            byte[] toByte;
            //a99f2883-8ed3-4583-afdb-3f570f159cf3
            using (WebClient client = new WebClient())
            {
                toByte = client.DownloadData((new Uri("https://localhost:44301/TrackingService.svc/Session/" + id)));
            }

            Stream strm = new MemoryStream(toByte);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(List<Models.TrackingModel.Session>));
            var objects = (List<Models.TrackingModel.Session>)obj.ReadObject(strm);
            foreach (var session in objects) {
                session.temp = GetAvgTempFromSessionDouble(session.Id).ToString();
                session.noAlerts = GetNoOfStatesInSessionInt(session.Id);
            }
            //session = (Models.TrackingModel.Session) objects;
            return objects;
        }

        public ActionResult GetSessionsBarChart(string id)
        {
            Models.TrackingModel.Session session;
           
            session = GetSession(id);
            var data = new List<Models.TrackingModel.TrackingTempBarChartModel>();
            //    new Models.TrackingModel.TrackingTempLineChartModel { date ="1", temp = "30"}
            //};
            foreach (Models.TrackingModel.TrackingState state in session.states)
            {
                if (state.time != null || state.stateType != "Session Started")
                {
                    data.Add(new Models.TrackingModel.TrackingTempBarChartModel(state.time, state.temp.ToString()));          
                }
            }

            JsonResult jr = new JsonResult();
            jr.Data = data;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        private int GetNoOfStatesInSessionInt(int id)
        {
            Models.TrackingModel.Session session;
            using (var db = new Models.ApplicationDbContext())
            {
                // Check for empty session
                try
                {
                    session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return session.states.Count;
        }


        public ActionResult GetNoOfStatesInSession(int id) {
            Models.TrackingModel.Session session;
            int length;
            JsonResult jr = new JsonResult();
            using (var db = new Models.ApplicationDbContext())
            {
              // Check for empty session
              try
              {
                session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
                length = session.states.Count;
               
                jr.Data = length;
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

              }
              catch (Exception)
              {   
                //Warning(ex.Message, true);
              }
              
            }

            //    new Models.TrackingModel.TrackingTempLineChartModel { date ="1", temp = "30"}
            //};
            return jr;
        }

        public double GetAvgTempFromSessionDouble(int id)
        {
            double averageTemp = 0;
            Models.TrackingModel.Session session;
            using (var db = new Models.ApplicationDbContext())
            {
                // Check for empty session
                try
                {
                    session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
                }
                catch (Exception)
                {
                    throw;
                }

            }
            foreach (var state in session.states)
            {
                averageTemp += state.temp;
            }

            averageTemp /= session.states.Count;

            averageTemp = Math.Round(averageTemp);
            return averageTemp;
        }

        public ActionResult GetAvgTempFromSession(int id)
        {
            double averageTemp = 0;
            Models.TrackingModel.Session session;
            JsonResult jr = new JsonResult();
            using (var db = new Models.ApplicationDbContext())
            {
              // Check for empty session
              try
              {
                session = db.Sessions.Include(x => x.states).Single(x => x.Id == id);
                foreach (var state in session.states)
                {
                    averageTemp += state.temp;
                }
                averageTemp /= session.states.Count;
                averageTemp = Math.Round(averageTemp);

                jr.Data = averageTemp;
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
              }
              catch (Exception)
              {
                //Warning(ex.Message, true);    
                //throw;
              }        
            }
           
            return jr;
        }

        public ActionResult ListOfStatesID()
        {
            try
            {
                //Warning("IN TRACKING FUNC");
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

        public ActionResult DeleteSession(int id) {
            try
            {
                using (var client = new WebClient()) {
                   // client.BaseAddress = new Uri("https://localhost:44301/TrackingService.svc/delete/" + id).ToString();
                    client.UploadString("https://localhost:44301/TrackingService.svc/Session/Delete/" + id, "DELETE", "");
                }

                Success("<strong>Success!</strong> Session Deleted", true);

                return RedirectToAction("Sessions");
            }
            catch (Exception ex) {
                Danger("<strong>Warning!</strong> " + ex.Message, true);
                return RedirectToAction("Sessions");
            }        
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