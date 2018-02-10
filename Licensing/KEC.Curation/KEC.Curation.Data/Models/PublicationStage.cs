using System.Collections.Generic;
using System.ComponentModel;

namespace KEC.Curation.Data.Models
{
    public enum PublicationStage
    {   
        [Description("Newly submitted document")]
        NewPublication = 0,
        [Description("Legal Verification")]
        LegalVerification = 1,
        [Description("Payment verification")]
        PaymentVerification = 2,
        [Description("Principal Curator Level")]
        PrincipalCuratorLevel = 3,
        [Description("Chief Curator Level")]
        ChiefCurator_New = 4,
        [Description("Under Curation")]
        Curation = 5,
        [Description("Sent To Chief Curator")]
        Curated = 6,
        [Description("Cheif Curator Approval")]
        ChiefCurator_Approved = 7,
        [Description("Approved")]
        Approved = 8,
        [Description("Rejected")]
        Rejected = 9,
        [Description("Certificate_Generated")]
        Certificate_Generated = 10
    }
}
