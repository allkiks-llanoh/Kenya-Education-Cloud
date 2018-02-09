using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;

namespace KEC.Curation.Web.Api.Serializers
{
    public class SectionsDownloadSerilizer
    {
        private readonly IUnitOfWork _uow;
        private readonly PublicationSection _section;

        public SectionsDownloadSerilizer(PublicationSection section, IUnitOfWork uow)
        {
            _uow = uow;
            _section = section;
        }
        public string SectionDescription
        {
            get
            {
                return _section.SectionDescription;
            }
        }
        public int Id
        {
            get
            {
                return _section.Id;
            }
        }
    }
}
