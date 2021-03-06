﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Net;

namespace TestClient
{
    [DataContractAttribute(Namespace = "http://schemas.datacontract.org/2004/07/TrackingRESTService.Model")]
    class TrackingState
    {
        [DataMemberAttribute]
        private int Id;
        [DataMemberAttribute]
        private int UserId;
        [DataMemberAttribute]
        private string time;
        [DataMemberAttribute]
        private string place;
        [DataMemberAttribute]
        private string temp;
        [DataMemberAttribute]
        private int noAlerts;

        public TrackingState(int UserId, string time, string place, double temp, int noAlerts) {
            this.UserId = UserId;
            this.time = time;
            this.place = place;
            this.temp = temp.ToString();
            this.noAlerts = noAlerts;
        }

        public int getId() {
            return Id;
        }

        public int getUserId() {
            return UserId;
        }

        public string getTime() {
            return time;
        }

        public string getPlace()
        {
            return place;
        }

        public string getTemp() {
            return temp;
        }
        public int getNoAlerts() {
            return noAlerts;
        }
    }
}
