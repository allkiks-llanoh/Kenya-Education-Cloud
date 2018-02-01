using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Curation.Data.Database
{
    public class CurationDataContext : DbContext
    {
        public CurationDataContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
