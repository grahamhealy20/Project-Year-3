namespace TrackingRESTService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class TrackingState
    {
        [Key]
        public int TrackingId { get; set; }
        public string UserId { get; set; }
        public string time { get; set; }
        public string place { get; set; }
        public double temp { get; set; }
        public int noAlerts { get; set; }
        public string stateType { get; set; }

        public TrackingState() { }

        public TrackingState(string userId ,string stateType) {
            this.UserId = userId;
            this.stateType = stateType;
        }
    }
}