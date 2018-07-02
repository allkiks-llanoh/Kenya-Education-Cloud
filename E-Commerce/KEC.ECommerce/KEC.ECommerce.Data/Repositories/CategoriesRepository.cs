using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class CategoriesRepository : Repository<Category>
    {
        private readonly ECommerceDataContext _eCommerceContext;

        public CategoriesRepository(ECommerceDataContext context) : base(context)
        {
            _eCommerceContext = context as ECommerceDataContext;
        }
        public Category AddPublicationCategory(string name)
        {
            var retrievedCategory = _eCommerceContext.Categories.FirstOrDefault(p => p.Name.Equals(name));
            if (retrievedCategory == null)
            {
                var category = new Category
                {
                    Name = name
                };
                _eCommerceContext.Categories.Add(category);
                _eCommerceContext.SaveChanges();
                return category;
            }
            else
            {
                return retrievedCategory;
            }

        }
    }
}
