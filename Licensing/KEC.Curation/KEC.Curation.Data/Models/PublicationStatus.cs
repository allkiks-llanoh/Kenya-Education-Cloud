
using System.ComponentModel;


namespace KEC.Curation.Data.Models
{
    public enum PublicationStatus
    {
        [Description("Published")]
        Published =0,
        [Description("Legal")]
        Legal = 1,
        [Description("Finance")]
        Finance = 2,
        [Description("Principal_New")]
        Principal_New = 3,
        [Description("ChiefCurator_New")]
        ChiefCurator_New= 4,
        [Description("Curation")]
        Curation = 5,
        [Description("ChiefCurator_Approved")]
        ChiefCurator_Approved = 6,
        [Description("Approved")]
        Approved = 7,
        [Description("Rejected")]
        Rejected = 8,
        [Description("Certificate_Generated")]
        Certificate_Generated = 9
    }
}
