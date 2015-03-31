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
        //private Sensor tempSensor = new Sensor();
        public delegate void TrackingHandler(object myObject,
                                             EventArgs myArgs);

        public event TrackingHandler onTrackingDetected;

        public int startTracking() {
            try {
                // Start Kinect
                sensor = new Kinect();
                sensor.Start();
                EventArgs eargs = new EventArgs();
                sensor.OnMotionDetected += new Kinect.DetectionHandler(FireTrackingEvent);
                // Start Temp
                //tempSensor.start();
                return 1;    

            } catch(Exception ex) {
                // Need to fire event to GUI with ex message.
                return 0;
            }
        }

        public TrackingState getLatestState() {
          userId = 1;
          time = new DateTime();
          time = DateTime.Now;

          place = "Dublin";
          //temp = tempSensor.getTemp();
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

        void FireTrackingEvent(object a, EventArgs e) {
            onTrackingDetected(a, e);
        } 
    }
}
