using KEC.Publishers.Data.Database;
using KEC.Publishers.Data.Helpers;
using KEC.Publishers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Publishers.Data.Repositories
{
    public class PublicationRepository : Repository<Publication>
    {
        public PublicationRepository(PublisherDataContext context) : base(context)
        {
        }
        public PublisherDataContext Context
        {
            get
            {
                return _context as PublisherDataContext;
            }
        }
        public string GetKICDNUmber(List<Publication> publications)
        {
            var kicdNumber = string.Empty;
            do
            {
                kicdNumber = RandomCodeGenerator.GetKICDNUmber("KEC");
            } while ((Find(p => p.KICDNumber.Equals(kicdNumber)).FirstOrDefault() != null) &&
            (publications.Where(p => p.KICDNumber.Equals(kicdNumber)).FirstOrDefault() != null));

            return kicdNumber;
        }
        public string GetContentNUmber(List<Publication> publications)
        {
            var contentNumber = string.Empty;
            do
            {
                contentNumber = RandomCodeGenerator.GetContentNUmber("KEC-");
            } while ((Find(p => p.CertificateNumber.Equals(contentNumber)).FirstOrDefault() != null) &&
            (publications.Where(p => p.CertificateNumber.Equals(contentNumber)).FirstOrDefault() != null));

            return contentNumber;
        }
        public bool CanProcessCurationPublication(Publication publication)
        {
            var canProcess = publication.FullyAssigned
                      && Context.CuratorAssignments.All(p => p.Submitted);
            return canProcess;

        }
        public void ProcessToTheNextStage(Publication publication)
        {
            var maxStage = Context.PublicationStageLogs
                               .Where(p => p.PublicationId.Equals(publication.Id))
                               .Max(p => p.Stage);
            var currentStage = (int)Context.PublicationStageLogs
                                           .First(p => p.PublicationId.Equals(publication.Id)
                                           && p.Stage == maxStage
                                           && p.ActionTaken != null
                                           && p.Owner != null).Stage;
            var nextStage = currentStage + 1;
            if (Enum.IsDefined(typeof(PublicationStage), nextStage))
            {
                var publicationStage = new PublicationStageLog
                {
                    PublicationId = publication.Id,
                    Stage = (PublicationStage)nextStage,
                    CreatedAtUtc = DateTime.UtcNow
                };
                Context.PublicationStageLogs.Add(publicationStage);
                Context.SaveChanges();
            }

        }
    }
}
