﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KEC.Curation.Data.Models
{
    public enum ActionTaken
    {
        [Description("New publication submitted")]
        PublicationSubmitted=0,
        [Description("Publication rejected")]
        PublicationRejected=1,
        [Description("Publication passed to the next stage")]
        PublicationMoveToNextStage=3,
        [Description("Publication Approved")]
        PublicationApproved = 4


    }
}
