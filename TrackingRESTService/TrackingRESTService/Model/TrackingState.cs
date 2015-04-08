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
        public string temp { get; set; }
        public int noAlerts { get; set; }
    }
}
