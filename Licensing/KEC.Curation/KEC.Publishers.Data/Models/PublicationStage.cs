using System.ComponentModel;

namespace KEC.Publishers.Data.Models
{
    public enum PublicationStage
    {
        [Description("Newly submitted document")]
        NewPublication = 1,
        [Description("Legal Verification")]
        LegalVerification = 2,
        [Description("Payment verification")]
        PaymentVerification = 3,
        [Description("Principal curator passes to chief curators")]
        PrincipalCurator = 4,
        [Description("Chief curator assigns to curators")]
        Curation = 5,
        [Description("Publication approval by principal curator")]
        PublicationApproval = 6,
        [Description("Issue of certificate")]
        IssueOfCertificate = 7
    }
}
