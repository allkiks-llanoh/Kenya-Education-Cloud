using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class ChiefCutatorDownloadSerilizer
    {
        private readonly ChiefCuratorAssignment _assignment;

        private readonly IUnitOfWork _uow;
        public  ChiefCutatorDownloadSerilizer(ChiefCuratorAssignment assignment, IUnitOfWork uow)
        {
           _assignment = assignment;
           _uow = uow;
        }

}
}
 