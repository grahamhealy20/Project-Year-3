using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AspNet.Identity;

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
          try { 
            objects = GetAllTrackingState();
            Success(string.Format("List successfully retrieved at: <strong>{0}</strong>", DateTime.Now.ToString()), true);
           }
          catch (Exception ex) { 
            Danger(string.Format("<strong>ERROR: </strong>{0}", ex.Message), true);
           }    
            // objects.Add(new Models.TrackingModel.TrackingState(2, "3:00", "Ballyfermot", 30.00, 4));
           
           
            return View(objects);

        }

        public ActionResult TrackingInfoID() {
            //state = GetTrackingState(Convert.ToInt32(User.Identity.GetUserId()));
            
            state = GetTrackingState(2147483647);
            return View(state);
        }

        public List<Models.TrackingModel.TrackingState> GetAllTrackingState() { 
            byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/GetAllTrackingState")));

            Stream strm = new MemoryStream(toByte); 
            DataContractSerializer obj = new DataContractSerializer(typeof(List<Models.TrackingModel.TrackingState>));
            var objects = (List<Models.TrackingModel.TrackingState>) obj.ReadObject(strm);

            Response.Cookies.Add(new HttpCookie("FlashMessage", "Success"));
            return objects;
        }

        public Models.TrackingModel.TrackingState GetTrackingState(int id)
        {
            byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/TrackingState/latest/" + id)));

            Stream strm = new MemoryStream(toByte);
            DataContractSerializer obj = new DataContractSerializer(typeof(Models.TrackingModel.TrackingState));
            var state = (Models.TrackingModel.TrackingState)obj.ReadObject(strm);

            Response.Cookies.Add(new HttpCookie("FlashMessage", "Success"));
            return state;
        }

        public string Welcome() {
            return "This is the welcome action method.";
        }
    }
}