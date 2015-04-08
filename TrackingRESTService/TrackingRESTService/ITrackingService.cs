using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace TrackingRESTService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITrackingService" in both code and config file together.
    [ServiceContract]
    public interface ITrackingService
    {
        // TRACKING STATE METHODS

        [OperationContract]
        [WebGet(UriTemplate = "TrackingState/Latest/{user_Id}")]
        Model.TrackingState GetLatestTrackingState(string user_Id);

        [OperationContract]
        [WebGet(UriTemplate = "TrackingState/{user_Id}")]
        List<Model.TrackingState> GetTrackingStatesByUser(string user_Id);

        [OperationContract]
        [WebGet]
        List<Model.TrackingState> GetAllTrackingState();

        [OperationContract]
        [WebGet(UriTemplate = "TrackingState?place={value}", ResponseFormat = WebMessageFormat.Json)]
        List<Model.TrackingState> GetTrackingStateByPlace(string value);

        [OperationContract]
        [WebInvoke(UriTemplate = "TrackingState/Add", Method = "POST")]
        int AddTrackingStatus(Model.TrackingState p);

        [OperationContract]
        [WebInvoke(UriTemplate = "TrackingState/Update", Method = "PUT")]
        bool UpdateState(Model.TrackingState patient);

        [OperationContract]
        [WebInvoke(UriTemplate = "TrackingState/Delete/{id}", Method = "DELETE")]
        bool DeleteState(string id);

        // SESSION METHODS

        [OperationContract]
        [WebGet(UriTemplate = "Session/LatestSession/{user_Id}", ResponseFormat = WebMessageFormat.Json)]
        Model.Session GetLatestSession(string user_Id);

        /* This method will return the latest state in the session for the user*/
        [OperationContract]
        [WebGet(UriTemplate = "Session/Latest/{user_Id}", ResponseFormat = WebMessageFormat.Json)]
        Model.TrackingState GetLatestSessionState(string user_Id);

        /* This method will return the all of the sessions for the specified user*/
        [OperationContract]
        [WebGet(UriTemplate = "Session/{user_id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Model.Session> GetSessions(string user_Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "Session/Add/", Method = "POST")]
        int AddSession(Model.Session toAdd);

        /* This method will return the all of the sessions for the specified user*/
        [OperationContract]
        [WebInvoke(UriTemplate = "Session/Add/TrackingState/", Method = "POST")]
        int AddStateToLatestSession(Model.TrackingState toAdd);


    }
}
