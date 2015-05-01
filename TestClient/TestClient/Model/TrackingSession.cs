using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Model
{
    class TrackingSession
    {
        private Model.ApplicationUser user;
        private Kinect sensor = new Kinect();
        private Sensor arduino = new Sensor();
        //private Sensor tempSensor = new Sensor();
        public delegate void TrackingHandler(object myObject, EventArgs myArgs);
        public event TrackingHandler onTrackingDetected;

        public delegate void TemperatureHandler(object myObject, TemperatureEventArgs myArgs);
        public event TemperatureHandler onTempDetected;

        public delegate void TemperatureGUIHandler(object myObject, TemperatureEventArgs myArgs);
        public event TemperatureGUIHandler onTempGUI;

        public delegate void StatusMessageHandler(object myObject, InfoEventArgs myArgs);
        public event StatusMessageHandler onInfoEvent;
    
        public TrackingSession() {
          sensor = new Kinect();
        }

        public TrackingSession(Model.ApplicationUser user_in) {
            user = user_in;
        }

        public int Stop()
        {
            try {
                // unsubscribe
                sensor.OnMotionDetected -= new Kinect.DetectionHandler(FireTrackingEvent);
                sensor.onInfoEvent += new Kinect.StatusMessageHandler(onInfoEvent);
                arduino.OnTemperatureReceived -= new Sensor.TemperatureHandler(FireTemperatureEvent);
                arduino.OnTemperatureGUIReceived -= new Sensor.TemperatureGUIHandler(UpdateGUITemp);
                sensor.Stop();
                arduino.Stop();
                return 1;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public int startTracking() {
            try {
                // Start Kinect
              try
              {
                sensor.StartKinect();
                sensor.OnMotionDetected += new Kinect.DetectionHandler(FireTrackingEvent);
              }
              catch (Exception ex)
              {

              }
                arduino.OnTemperatureReceived += new Sensor.TemperatureHandler(FireTemperatureEvent);
                arduino.OnTemperatureGUIReceived += new Sensor.TemperatureGUIHandler(UpdateGUITemp);
                arduino.Start();
                startSession();
                return 1;    

            } catch(Exception ex) {
                // Need to fire event to GUI with ex message.
                return 0;
            }
        }

        public int startSession()
        {
            Session sess = new Session(user.Id);
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


        // EVENT HANDLERS

        void FireTrackingEvent(object a, EventArgs e) {
            // Call Rest call to add state
            Model.TrackingState state = new Model.TrackingState(user.Id, DateTime.Now.ToString(), "Dublin", arduino.GetTemperature(), 1, "Motion Event");
            //addState(state);
            RESTConsume.AddState(state);
            onTrackingDetected(a, e);
        }

        private void FireTemperatureEvent(object myObject, TemperatureEventArgs myArgs)
        {
            // Call Rest call to add state
            Model.TrackingState state = new Model.TrackingState(user.Id, DateTime.Now.ToString(), "Dublin", 30, 1, "Temperture Event");
            onTempDetected(myObject, myArgs);
        }

        private void UpdateGUITemp(object myObject, TemperatureEventArgs myArgs)
        {
            try
            {
                onTempGUI(myObject, myArgs);
            }
            catch (Exception)
            {             
                //throw;
            }

        }

    }
}
