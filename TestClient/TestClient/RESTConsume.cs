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
       
        public static int StartSession(Model.Session session) {
            try {
                DataContractSerializer ser = new DataContractSerializer(typeof(Model.Session));
                MemoryStream strm = new MemoryStream();
                ser.WriteObject(strm, session);
                string data = Encoding.UTF8.GetString(strm.ToArray(), 0, (int)strm.Length);
                // Custom headers
                proxy.Headers["Content-type"] = "application/xml";
                // Encoding
                proxy.Encoding = Encoding.UTF8;
                var response = proxy.UploadString("https://localhost:44301/TrackingService.svc/Session/Add", "POST", data);                
                return 1;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public static int AddState(Model.TrackingState state)
        {
            try
            {
                using (WebClient client = new WebClient()) {
                    DataContractSerializer ser = new DataContractSerializer(typeof(Model.TrackingState));
                    MemoryStream strm = new MemoryStream();
                    ser.WriteObject(strm, state);
                    string data = Encoding.UTF8.GetString(strm.ToArray(), 0, (int)strm.Length);
                    // Custom headers
                    client.Headers["Content-type"] = "application/xml";
                    // Encoding
                    client.Encoding = Encoding.UTF8;
                    client.UploadString("https://localhost:44301/TrackingService.svc/Session/Add/TrackingState/", "POST", data);
                    return 1;
                }      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
