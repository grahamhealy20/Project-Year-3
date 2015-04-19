using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.TrackingModel
{
    public class TrackingViewModel
    {
    }

    [Serializable]
    public class TrackingTempLineChartModel {
        public string date {get; set;}
        public string temp {get; set;}

        public TrackingTempLineChartModel() { }
        public TrackingTempLineChartModel(string time, string temp) {
            this.date = time;
            this.temp = temp;
        }
    }
}