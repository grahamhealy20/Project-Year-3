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
        [OperationContract]
        [WebGet(UriTemplate = "TrackingState/DoWork")]
        void DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "TrackingState/Latest/{user_Id}")]
        Model.TrackingState GetLatestTrackingState(string user_Id);

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
    }
}
