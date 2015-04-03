namespace TrackingRESTService.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContractAttribute]
    public partial class TrackingState
    {
        [DataMemberAttribute]
        public int Id { get; set; }
        [DataMemberAttribute]
        public string UserId { get; set; }
        [DataMemberAttribute]
        public string time { get; set; }
        [DataMemberAttribute]
        public string place { get; set; }
        [DataMemberAttribute]
        public string temp { get; set; }
        [DataMemberAttribute]
        public int noAlerts { get; set; }
    }
}
