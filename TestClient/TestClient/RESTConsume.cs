using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;

namespace TestClient
{
    class RESTConsume
    {
        private static WebClient proxy = new WebClient();

        public static TrackingState getTrackingState(int id) {
                string userId = Convert.ToString(id);
                byte[] toByte = proxy.DownloadData((new Uri("http://localhost:4082/TrackingService.svc/TrackingState/Latest/" + id)));

                Stream strm = new MemoryStream(toByte);
                //return obj.ReadStream(strm).

                DataContractSerializer obj = new DataContractSerializer(typeof(TrackingState));
                TrackingState obje = (TrackingState)obj.ReadObject(strm);
                //TrackingState state = (TrackingState) obje;
                return obje;      
            }

        public static int AddState(TrackingState state) {
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TrackingState));
            DataContractSerializer ser = new DataContractSerializer(typeof(TrackingState));
            MemoryStream strm = new MemoryStream();
            ser.WriteObject(strm, state);
            string data = Encoding.UTF8.GetString(strm.ToArray(), 0, (int) strm.Length);
            proxy.Headers["Content-type"] = "application/xml";
            proxy.Encoding = Encoding.UTF8;
            proxy.UploadString("http://localhost:4082/TrackingService.svc/TrackingState/Add", "POST", data);
            return 1;
        }
    }
}
