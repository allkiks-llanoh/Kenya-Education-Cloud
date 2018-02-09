using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CuratorDownloadSerilizer
    {
        private readonly IUnitOfWork _uow;
        private readonly CuratorCreation  _curator;
        public CuratorDownloadSerilizer(CuratorCreation curator, IUnitOfWork uow)
        {
            _uow = uow;
            _curator = curator;
        }

        public int Id
        {
            get
            {
                return _curator.Id;
            }
        }
        public int TypeId
        {
            get
            {
                return _curator.TypeId;
            }
        }
        public string FirstName
        {
            get
            {
                return _curator.FirstName;
            }
        }
        public string LastName
        {
            get
            {
                return _curator.LastName;
            }
        }
    }
}
