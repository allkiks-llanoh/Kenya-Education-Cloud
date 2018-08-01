﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Models
{
   public class ChiefCuratorComment
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string Notes { get; set; }
        public string ChiefCuratorGuid { get; set; }
        public bool Submitted  { get; set; }
        public string Recomendation { get; set; }
        public string FullName { get; set; }

    }
}
