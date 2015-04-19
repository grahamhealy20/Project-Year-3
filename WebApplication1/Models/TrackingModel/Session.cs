namespace WebApplication1.Models.TrackingModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Session
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime time { get; set; }

        public string place { get; set; }

        public string temp { get; set; }

        public int noAlerts { get; set; }

        public int Session_Id { get; set; }
    }
}
