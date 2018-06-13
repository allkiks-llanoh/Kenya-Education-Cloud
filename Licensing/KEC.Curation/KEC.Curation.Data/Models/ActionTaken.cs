using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
namespace KEC.Curation.Data.Models
{
    public enum ActionTaken
    {
        [Description("New publication submitted")]
        PublicationSubmitted=0,
        [Description("Publication recommended for rejection")]
        PublicationRejected=1,
        [Description("Publication recommended for approval")]
        PublicationApproved = 2,
        [Description("Publication passed to the next stage")]
        PublicationMoveToNextStage=3,
        [Description("Publication has been approved")]
        Approved = 4,
        [Description("Publication conditional approval")]
        PublicationConditionalApproval = 5,
        [Description("Publication passed to Publisher")]
        PublicationReturnedToPublisher = 6,
    }
}
