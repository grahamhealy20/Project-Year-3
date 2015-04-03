using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace TrackingRESTService.Model
{
    [DataContractAttribute]
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

        public Session(string UserId, DateTime time, string place)
        {
            this.UserId = UserId;
            this.time = time;
            this.place = place;
            this.temp = "0.00";
            this.noAlerts = 0;
        }
    }
}