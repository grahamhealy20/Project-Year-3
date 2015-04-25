namespace WebApplication1.Models.TrackingModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Session
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime time { get; set; }

        public string place { get; set; }

        public string temp { get; set; }

        public int noAlerts { get; set; }

        public List<TrackingState> states { get; set; }
    }
}