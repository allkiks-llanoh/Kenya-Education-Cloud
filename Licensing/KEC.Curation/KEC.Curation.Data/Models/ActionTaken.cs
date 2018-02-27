﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using KEC.Curation.Services.Extensions;
namespace KEC.Curation.Data.Models
{
    public enum ActionTaken
    {
        [Description("New publication submitted")]
        PublicationSubmitted=0,
        [Description("Publication rejected")]
        PublicationRejected=1,
        [Description("Publication approved")]
        PublicationApproved = 2,
        [Description("Publication passed to the next stage")]
        PublicationMoveToNextStage=3,
       
        [Description("Publication conditional approval")]
        PublicationConditionalApproval = 5
      
    }
}
