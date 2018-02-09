using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;


namespace KEC.Curation.Web.Api.Serializers
{
    public class LevelsDownloadSerilizer
    {
        private readonly IUnitOfWork _uow;
        private readonly Level _level;
        
        public LevelsDownloadSerilizer(Level level, IUnitOfWork uow)
        {
            _uow = uow;
            _level = level;
        }
        public string Name
        {
            get
            {
                return _level.Name;
            }
        }
        public int Id
        {
            get
            {
                return _level.Id;
            }
        }
    }
}
