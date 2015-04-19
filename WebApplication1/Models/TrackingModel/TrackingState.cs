namespace WebApplication1.Models.TrackingModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TrackingState
    {
        public string UserId { get; set; }

        public string time { get; set; }

        public string place { get; set; }

        public int noAlerts { get; set; }

        public int Session_Id { get; set; }

        [Key]
        public int TrackingId { get; set; }

        public double temp { get; set; }

        public string stateType { get; set; }
    }
}
