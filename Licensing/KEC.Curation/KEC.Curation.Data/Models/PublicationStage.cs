using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KEC.Curation.Data.Models
{
    public enum PublicationStage
    {
        [Description("Newly submitted document")]
        NewPublication=1,
        [Description("Legal Verification")]
        LegalVerification=2,
        [Description("Payment verification")]
        PaymentVerification = 3,
        [Description("Publication Curation")]
        PublicationCuration = 4,
        [Description("Publication Approval")]
        PublicationApproval = 5
    }
}
