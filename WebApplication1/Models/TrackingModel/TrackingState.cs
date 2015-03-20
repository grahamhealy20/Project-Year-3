using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.TrackingModel
{
   [DataContractAttribute(Namespace = "http://schemas.datacontract.org/2004/07/TrackingRESTService.Model")]
    public class TrackingState
    {

        [DataMemberAttribute]
        public int Id { get; set; }
        [DataMemberAttribute]
        public int UserId { get; set; }
        [DataMemberAttribute]
        public string time { get; set; }
        [DataMemberAttribute]
        public string place { get; set; }
        [DataMemberAttribute]
        public string temp { get; set; }
        [DataMemberAttribute]
        public int noAlerts { get; set; }

        public TrackingState(int UserId, string time, string place, double temp, int noAlerts)
        {
            this.UserId = UserId;
            this.time = time;
            this.place = place;
            this.temp = temp.ToString();
            this.noAlerts = noAlerts;
        }

  
    }
}