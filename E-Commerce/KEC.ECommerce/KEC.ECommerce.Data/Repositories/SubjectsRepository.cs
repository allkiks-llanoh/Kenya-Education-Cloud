using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class SubjectsRepository : Repository<Subject>
    {
        private readonly ECommerceDataContext _eCommerceContext;

        public SubjectsRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceContext = context as ECommerceDataContext;
        }
        public Subject AddPublicationSubject(string name)
        {
            var retrievedSubject = _eCommerceContext.Subjects.FirstOrDefault(p => p.Name.Equals(name));
            if (retrievedSubject == null)
            {
                var subject = new Subject
                {
                    Name = name
                };
                _eCommerceContext.Subjects.Add(subject);
                _eCommerceContext.SaveChanges();
                return subject;
            }
            else
            {
                return retrievedSubject;
            }
        }

    }
}
