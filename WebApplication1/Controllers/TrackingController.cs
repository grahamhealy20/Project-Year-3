using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Runtime.Serialization;

namespace WebApplication1.Controllers
{
    public class TrackingController : Controller
    {
        private static WebClient proxy = new WebClient();
        private List<Models.TrackingModel.TrackingState> objects = new List<Models.TrackingModel.TrackingState>();


        // GET: Tracking
        public ActionResult Index()
        {
            // objects.Add(new Models.TrackingModel.TrackingState(2, "3:00", "Ballyfermot", 30.00, 4));
            objects = GetAllTrackingState();
            return View(objects);

        }

        public List<Models.TrackingModel.TrackingState> GetAllTrackingState() { 
            byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/GetAllTrackingState")));

            Stream strm = new MemoryStream(toByte); 
            DataContractSerializer obj = new DataContractSerializer(typeof(List<Models.TrackingModel.TrackingState>));
            var objects = (List<Models.TrackingModel.TrackingState>) obj.ReadObject(strm);

            Response.Cookies.Add(new HttpCookie("FlashMessage", "Success"));
            return objects;
        }

        public string Welcome() {
            return "This is the welcome action method.";
        }
    }
}