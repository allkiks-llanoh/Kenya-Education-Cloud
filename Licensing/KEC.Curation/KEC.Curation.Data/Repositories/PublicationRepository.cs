using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using KEC.Curation.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationRepository : Repository<Publication>
    {
        public PublicationRepository(CurationDataContext context) : base(context)
        {
        }
        public CurationDataContext Context
        {
            get
            {
                return _context as CurationDataContext;
            }
        }
        public string GetKICDNUmber(List<Publication> publications)
        {
            var kicdNumber = string.Empty;
            do
            {
                kicdNumber = RandomCodeGenerator.GetKICDNUmber("KICD");
            } while ((Find(p => p.KICDNumber.Equals(kicdNumber)).FirstOrDefault() != null) &&
            (publications.Where(p => p.KICDNumber.Equals(kicdNumber)).FirstOrDefault() != null));

            return kicdNumber;
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
