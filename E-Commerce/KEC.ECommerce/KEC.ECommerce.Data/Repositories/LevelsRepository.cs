using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class LevelsRepository : Repository<Level>
    {
        private readonly ECommerceDataContext _eCommerceContext;

        public LevelsRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceContext = context as ECommerceDataContext;
        }
        public Level AddPublicationLevel(string name)
        {
            var retrievedLevel = _eCommerceContext.Levels.FirstOrDefault(p => p.Name.Equals(name));
            if (retrievedLevel == null)
            {
                var level = new Level
                {
                    Name = name
                };
                _eCommerceContext.Levels.Add(level);
                _eCommerceContext.SaveChanges();
                return level;
            }
            else
            {
                return retrievedLevel;
            }
        }
    }
}
