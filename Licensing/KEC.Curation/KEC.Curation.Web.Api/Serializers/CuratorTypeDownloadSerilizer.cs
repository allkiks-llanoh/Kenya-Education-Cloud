using KEC.Curation.Data.Models;
using KEC.Curation.Data.Repositories;

namespace KEC.Curation.Web.Api.Serializers
{
    public class CuratorTypeDownloadSerilizer
    {
        private readonly CuratorType _curatorType;  
        public CuratorTypeDownloadSerilizer(CuratorType curatorType)
        {
          
            _curatorType = curatorType;
        }

        public CuratorTypeDownloadSerilizer(CuratorTypeRepository p)
        {
        }

        public string TypeName
        {
            get
            {
                return _curatorType.TypeName;
            }
        }
        public int Id
        {
            get
            {
                return _curatorType.Id;
            }
        }
    }
}
