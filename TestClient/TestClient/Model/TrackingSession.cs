using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Model
{
    class TrackingSession
    {
        private string userId;
        private DateTime time;
        private string place;
        private double temp;
        private int noAlert;
        private Kinect sensor = new Kinect();
        //private Sensor tempSensor = new Sensor();
        public delegate void TrackingHandler(object myObject, EventArgs myArgs);
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
                // Start Session on REST
                return 1;    

            } catch(Exception ex) {
                // Need to fire event to GUI with ex message.
                return 0;
            }
        }

        public int startSession()
        {
            Session sess = new Session("7dad98de-37f9-4830-a56b-83c1aef20a14");
            RESTConsume.StartSession(sess);
            return 1;
        }

        public int addState(Model.TrackingState state) {
            try {
               RESTConsume.AddState(state);
               return 1;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        void FireTrackingEvent(object a, EventArgs e) {
            // Call Rest call to add state
            //Model.TrackingState state = new Model.TrackingState("7dad98de-37f9-4830-a56b-83c1aef20a14", DateTime.Now.ToString(), "Dublin", 30, 1, "Motion Event");
            //addState(state);
            onTrackingDetected(a, e);
        } 
    }
}
