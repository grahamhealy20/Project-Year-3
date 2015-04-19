using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.TrackingModel
{
    [DataContractAttribute(Namespace = "http://schemas.datacontract.org/2004/07/TrackingRESTService.Model")]
    public class Session
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

        public Session(string UserId, DateTime time, string place, double temp, int noAlerts)
        {
            this.UserId = UserId;
            this.time = time;
            this.place = place;
            this.temp = temp.ToString();
            this.noAlerts = noAlerts;
        }

        public void addState(TrackingState state) {
            states.Add(state);
        }



    }
}