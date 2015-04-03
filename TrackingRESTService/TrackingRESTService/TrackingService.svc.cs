using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;
using System.Data.Entity;

namespace TrackingRESTService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TrackingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TrackingService.svc or TrackingService.svc.cs at the Solution Explorer and start debugging.
    public class TrackingService : ITrackingService
    {
        private int lastId;
        private Model.TrackingState latest;

        // TRACKING STATE METHODS

        public Model.TrackingState GetLatestTrackingState(string user_Id)
        {
            try
            {
                //int lastId = Convert.ToInt32(lastAddedID);
                using (var db = new Model.TrackingContext())
                {
                    List<Model.TrackingState> list = db.TrackingStates.Where(p => p.UserId == user_Id).ToList();
                   Model.TrackingState state = list.Last();

                   state.place = "HELLO TEST";
                   return state;
                }
            }
            catch (Exception ex)
            {
               //return FaultException(ex.Message);
               return null;
            }
        }

        public List<Model.TrackingState> GetTrackingStatesByUser(string user_Id)
        {
            try
            {
                //Parse string to int
                //int lastId = Convert.ToInt32(lastAddedID);
                using (var db = new Model.TrackingContext())
                {
                    List<Model.TrackingState> list = db.TrackingStates.Where(p => p.UserId == user_Id).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                //return FaultException(ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public List<Model.TrackingState> GetAllTrackingState()
        {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    return db.TrackingStates.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public List<Model.TrackingState> GetTrackingStateByPlace(string value)
        {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    return db.TrackingStates.Where(p => p.place == value).ToList();                 
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int AddTrackingStatus(Model.TrackingState p)
        {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    db.TrackingStates.Add(p);
                    db.SaveChanges();
                    lastId = p.Id;
                    latest = p;
                    //return p.Id;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool UpdateState(Model.TrackingState state)
        {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    Model.TrackingState oldDetails = db.TrackingStates.SingleOrDefault(p => p.Id == state.Id);

                    oldDetails.UserId = state.UserId;
                    oldDetails.time = state.time;
                    oldDetails.noAlerts = state.noAlerts;

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool DeleteState(string id)
        {
            try
            {
                int stateId = Convert.ToInt32(id);

                using (var db = new Model.TrackingContext())
                {
                    Model.TrackingState details = db.TrackingStates.SingleOrDefault(p => p.Id == stateId);
                    db.TrackingStates.Remove(details);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        // SESSION METHODS


        public Model.Session GetLatestSession(string user_Id) {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    Debug.WriteLine("IN METHOD");
                    return db.Sessions.Include(s => s.states).Where(p => p.UserId == user_Id).OrderByDescending(p => p.Id).First();
                    //Debug.WriteLine("CALLING OBJ");
                    //return db.Sessions.Include(s => s.states).First();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
                //return null;
            }
        }

        public Model.TrackingState GetLatestSessionState(string user_Id)
        {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    Debug.WriteLine("IN METHOD");

                    // Get latest session
                    Model.Session session = db.Sessions.Include(s => s.states).Where(p => p.UserId == user_Id).OrderByDescending(p => p.Id).First();
                    //Then access last state in list

                    Model.TrackingState state = session.states.Last();
                    state.place = "HELLO TEST";

                    return state;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /* This method will return the all of the sessions for the specified user*/
        public List<Model.Session> GetSessions(string user_Id) {
            try
            {
                //Parse string to int
                //int lastId = Convert.ToInt32(lastAddedID);
                using (var db = new Model.TrackingContext())
                {
                    return db.Sessions.Include(s => s.states).Where(p => p.UserId == user_Id).ToList();
                }
            }
            catch (Exception ex)
            {
                //return FaultException(ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public int AddSession(Model.Session toAdd) {
            try
            {
                using (var db = new Model.TrackingContext())
                {
                    db.Sessions.Add(toAdd);
                    db.SaveChanges();
                    //return p.Id;
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /* This method will add a state to the latest session*/
        public int AddStateToLatestSession(Model.TrackingState toAdd) { 
            // Get session
            try
            {
                using(var db = new Model.TrackingContext()) {
                    //List<Model.Session> session = db.Sessions.Where(p => p.UserId == user_Id).ToList();
                    db.Sessions.OrderByDescending(p => p.Id);
                    Model.Session session = db.Sessions.First(p => p.UserId == user_Id);

                    session.states.Add(toAdd);
                    db.SaveChanges();
                    //Model.Session singleSession = session.Last();
                    //singleSession.states.Add(toAdd);
                    //db.Sessions.
                    //db.Sessions.Add(singleSession);
                    return 1;
                }
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }
           
        }


    }
}
