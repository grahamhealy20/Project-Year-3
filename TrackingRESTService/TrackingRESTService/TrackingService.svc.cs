using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TrackingRESTService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TrackingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TrackingService.svc or TrackingService.svc.cs at the Solution Explorer and start debugging.
    public class TrackingService : ITrackingService
    {
        private int lastId;
        private Model.TrackingState latest;

        // TEST METHOD
        public void DoWork()
        {
            using (var db = new Model.TrackingContext())
            {
              
            }
        }

        public Model.TrackingState GetLatestTrackingState(string user_Id)
        {
            try
            {
            
                //Parse string to int
                int userId = Convert.ToInt32(user_Id);
                //int lastId = Convert.ToInt32(lastAddedID);
                using (var db = new Model.TrackingContext())
                {
                   List<Model.TrackingState> list = db.TrackingStates.Where(p => p.UserId == userId).ToList();
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


    }
}
