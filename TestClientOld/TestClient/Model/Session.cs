using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TestClient.Model
{
    [DataContractAttribute(Namespace = "http://schemas.datacontract.org/2004/07/TrackingRESTService.Model")]
    public partial class Session
    {
        [DataMemberAttribute]
        public int Id { get; set; }
        [DataMemberAttribute]
        public string UserId { get; set; }
        [DataMemberAttribute]
        public DateTime time { get; set; }
        [DataMemberAttribute]
        public string place { get; set; }
        [DataMemberAttribute]
        public string temp { get; set; }
        [DataMemberAttribute]
        public int noAlerts { get; set; }
        [DataMemberAttribute]
        public List<TrackingState> states { get; set; }

        public Session(string userId) {
            this.Id = 0;
            this.UserId = userId;
            this.time = DateTime.Now;
            place = "";
            temp = "";
            noAlerts =0;
            this.states = new List<TrackingState>();
        }
    }


}
