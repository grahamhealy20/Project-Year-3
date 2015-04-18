using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net;

namespace TestClient.Model
{
    [DataContractAttribute(Namespace = "http://schemas.datacontract.org/2004/07/TrackingRESTService.Model")]
     public class TrackingState
    {
        [DataMemberAttribute]
        public int TrackingId { get; set; }
        [DataMemberAttribute]
        public string UserId { get; set; }
        [DataMemberAttribute]
        public string time { get; set; }
        [DataMemberAttribute]
        public string place { get; set; }
        [DataMemberAttribute]
        public double temp { get; set; }
        [DataMemberAttribute]
        public int noAlerts { get; set; }
        [DataMemberAttribute]
        public string stateType { get; set; }

        public TrackingState(string UserId, string time, string place, double temp, int noAlerts, string stateType) {
            this.UserId = UserId;
            this.time = time;
            this.place = place;
            this.temp = temp;
            this.noAlerts = noAlerts;
            this.stateType = stateType;
        }

        public int getId() {
            return TrackingId;
        }

        public string getUserId() {
            return UserId;
        }

        public string getTime() {
            return time;
        }

        public string getPlace()
        {
            return place;
        }

        public double getTemp() {
            return temp;
        }
        public int getNoAlerts() {
            return noAlerts;
        }
    }
}
