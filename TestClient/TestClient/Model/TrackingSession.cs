using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Model
{
    class TrackingSession
    {
        private int userId;
        private DateTime time;
        private string place;
        private double temp;
        private int noAlert;

        private Kinect sensor = new Kinect();
        private Sensor tempSensor = new Sensor();
        public int startTracking() {
            try {
                // Start Kinect
                sensor = new Kinect();
                
                // Start Temp
                tempSensor.start();
                return 1;    

            } catch(Exception ex) {
                return 0;
            }
        }

        public TrackingState getLatestState() {
          userId = 1;
          time = new DateTime();
          time = DateTime.Now;

          place = "Dublin";
          temp = tempSensor.getTemp();
          if (sensor.getDetected() == true)
          {
            noAlert++;
          }
          else { 
          
          }
          string timestr = time.ToString(); 
          TrackingState state = new TrackingState(userId, timestr, place, temp, noAlert);
          return state;
        }



    }
}
