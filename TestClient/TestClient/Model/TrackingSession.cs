using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Model
{
    class TrackingSession
    {
        private Kinect sensor;
        public int startTracking() {
            try {
                // Start Kinect
                sensor = new Kinect();

                // Start Temp
                return 1;    

            } catch(Exception ex) {
                return 0;
            }
        }
      




    }
}
